using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApi.EMILAEntities;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Accounts;
using WebApi.Services.Interface;
using WebApi.ViewModel;
using BC = BCrypt.Net.BCrypt;

namespace WebApi.Services.Implementation
{
    public class AuthenticateService : IAuthenticateService
    {
        #region Feilds
        private readonly emiladbContext _dbContext;

        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;
        #endregion

        #region Constructor
        public AuthenticateService(emiladbContext dbContext,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IEmailService emailService

          )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _emailService = emailService;

        }
        #endregion

        #region PublicMethod

        public ServiceResponse<SignupVM> Register(SignupVM model, string origin)
        {
            ServiceResponse<SignupVM> _response = new ServiceResponse<SignupVM>();

            try
            {
                // validate
                if (_dbContext.TblUserMasters.Any(x => x.UserEmail == model.Email))
                {
                    //send already registered error in email to prevent account enumeration
                    sendAlreadyRegisteredEmail(model.Email, origin);
                    _response.ErrorCode = "-1";
                    _response.Message = "Email Already Registerd";
                }
                else
                {
                    // map model to new account object
                    //var account = _mapper.Map<Account>(model);

                    // first registered account is an admin
                    //var isFirstAccount = _context.Accounts.Count() == 0;

                    //using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    //{
                    //add data into teanant
                    var _objtenant = new TblTenantMaster();
                    _objtenant.IsTrial = model.IsTrial;
                    _objtenant.TenantName = model.TenantName;

                    _dbContext.TblTenantMasters.Add(_objtenant);
                    _dbContext.SaveChanges();

                    //add data into company
                    var _objcompany = new TblCompanyMaster();
                    _objcompany.CompanyName = model.TenantName + " Company";
                    _objcompany.TenantId = _objtenant.TenantId;
                    _objcompany.CreatedAt = System.DateTime.Now;

                    _dbContext.TblCompanyMasters.Add(_objcompany);
                    _dbContext.SaveChanges();

                    //add data into branch
                    var _objbranch = new TblBranchMaster();
                    _objbranch.BranchName = model.TenantName + " Company Branch";
                    _objbranch.CompanyId = _objcompany.CompanyId;
                    _objbranch.CreatedAt = System.DateTime.Now;

                    _dbContext.TblBranchMasters.Add(_objbranch);
                    _dbContext.SaveChanges();


                    //add data into tenant persona
                    var _listPersona = _dbContext.TblUserPersonas.ToList();

                    var _listTenantPersona = new List<TblTenantUserPersona>();

                    foreach (var item in _listPersona)
                    {
                        var _objTeantPersona = _mapper.Map<TblTenantUserPersona>(item);
                        _objTeantPersona.TenantId = _objtenant.TenantId;
                        _listTenantPersona.Add(_objTeantPersona);
                    }

                    _dbContext.TblTenantUserPersonas.AddRange(_listTenantPersona);
                    _dbContext.SaveChanges();

                    //add data into user master

                    var _tenentPersona = _dbContext.TblTenantUserPersonas.Where(x => x.TenantId == _objtenant.TenantId &&
                     x.PersonaShortname == "SADM").FirstOrDefault();

                    var _objuser = new TblUserMaster();
                    _objuser.BranchId = _objbranch.BranchId;
                    _objuser.PersonaId = _tenentPersona.TenantPersonaId;
                    _objuser.VerificationToken = randomTokenString();
                    _objuser.FirstName = model.FirstName;
                    _objuser.LastName = model.LastName;
                    _objuser.UserEmail = model.Email;
                    _objuser.UserInitials = model.Initials;
                    _objuser.Phone = model.Phone;
                    _objuser.IsVerified = false;

                    // hash password
                    _objuser.UserPassword = BC.HashPassword(model.Password);

                    // save account
                    _dbContext.TblUserMasters.Add(_objuser);
                    _dbContext.SaveChanges();



                    //add data into tenant task
                    var _listTasks = _dbContext.TblTasksDefalts.ToList();

                    var _listTenantTaks = new List<TblTenantTask>();

                    foreach (var item in _listTasks)
                    {
                        var _objTeantTask = _mapper.Map<TblTenantTask>(item);
                        _objTeantTask.TenantId = _objtenant.TenantId;
                        _listTenantTaks.Add(_objTeantTask);
                    }


                    _dbContext.TblTenantTasks.AddRange(_listTenantTaks);
                    _dbContext.SaveChanges();


                    //add data into tenant milestone
                    var _listMileStone = _dbContext.TblMilestones.ToList();

                    var _listTenantMileStone = new List<TblTenantMilestone>();

                    foreach (var item in _listMileStone)
                    {
                        var _objTeantMileStone = _mapper.Map<TblTenantMilestone>(item);
                        _objTeantMileStone.TenantId = _objtenant.TenantId;
                        _listTenantMileStone.Add(_objTeantMileStone);
                    }

                    _dbContext.TblTenantMilestones.AddRange(_listTenantMileStone);
                    _dbContext.SaveChanges();



                    //add data into tenant needs
                    var _listNeeds = _dbContext.TblNeedsDefalts.ToList();

                    var _listTenantNeeds = new List<TblTenantNeed>();

                    foreach (var item in _listNeeds)
                    {
                        var _objTenantNeeds = _mapper.Map<TblTenantNeed>(item);
                        _objTenantNeeds.TenantId = _objtenant.TenantId;
                        _listTenantNeeds.Add(_objTenantNeeds);
                    }


                    _dbContext.TblTenantNeeds.AddRange(_listTenantNeeds);
                    _dbContext.SaveChanges();



                    _response.Success = true;
                    _response.ErrorCode = "1";
                    _response.Message = "Registration successful, please check your email for verification instructions";
                    //}

                    // send email
                    sendVerificationEmail(_objuser.VerificationToken, _objuser.UserEmail, origin, _objuser.FirstName);
                }
            }
            catch (Exception ex)
            {
                _response.ErrorCode = "0";
                _response.Message = "something went wrong!!";
            }

            return _response;
        }

        public ServiceResponse<UserMasterVM> Login(LoginVM model, string ipAddress)
        {
            ServiceResponse<UserMasterVM> _response = new ServiceResponse<UserMasterVM>();

            try
            {

                var account = _dbContext.TblUserMasters.SingleOrDefault(x => x.UserEmail == model.Email);

                var _objVM = _mapper.Map<UserMasterVM>(account);

                if (account == null || !account.IsVerified.Value || !BC.Verify(model.Password, account.UserPassword))
                {
                    if (account.IsVerified.Value)
                    {
                        _response.Message = "Email or password is incorrect";
                        _response.ErrorCode = "-1";
                    }
                    else
                    {
                        _response.Message = "Account still not verify.";
                        _response.ErrorCode = "-2";
                    }
                    _response.Success = false;

                }
                else
                {
                    // authentication successful so generate jwt and refresh tokens

                    var tenant = from t in _dbContext.TblTenantMasters
                                 join c in _dbContext.TblCompanyMasters on t.TenantId equals c.TenantId
                                 join b in _dbContext.TblBranchMasters on c.CompanyId equals b.CompanyId
                                 join u in _dbContext.TblUserMasters on b.BranchId equals u.BranchId
                                 where u.UserId == _objVM.UserId
                                 select t;

                    _objVM.TenantId = tenant.FirstOrDefault().TenantId;

                    var jwtToken = generateJwtToken(_objVM);
                    var refreshToken = generateRefreshToken(ipAddress);

                    var _tenentPersona = _dbContext.TblTenantUserPersonas.Where(x => x.TenantId == tenant.FirstOrDefault().TenantId &&
                   x.PersonaShortname == "SADM").FirstOrDefault();


                    if (account.PersonaId == _tenentPersona.TenantPersonaId && (account.IsFirstLogin == null))
                        account.IsFirstLogin = true;
                    else
                        account.IsFirstLogin = false;

                    account.VerificationToken = refreshToken.Token;
                    _objVM.VerificationToken = refreshToken.Token;
                    _objVM.IsFirstLogin = account.IsFirstLogin;
                    // save changes to db
                    _dbContext.Update(account);
                    _dbContext.SaveChanges();

                    _response.Message = "Login Successfully.";
                    _response.Success = true;
                    _response.ErrorCode = "1";
                    _response.jwtToken = jwtToken;
                    _response.refreshToken = refreshToken.Token;
                    _response.Id = account.UserId;
                    _response.Model = _objVM;

                    this.loginAudit(ipAddress, account.UserId);

                }
            }
            catch (Exception ex)
            {
                _response.ErrorCode = "0";
                _response.Message = "something went wrong!!";


            }
            return _response;
        }

        public ServiceResponse<bool> VerifyEmail(string token)
        {
            ServiceResponse<bool> _response = new ServiceResponse<bool>();
            var account = _dbContext.TblUserMasters.SingleOrDefault(x => x.VerificationToken == token);
            try
            {
                if (account == null)
                {
                    _response.Message = "Verification failed";
                    _response.Success = false;
                    _response.ErrorCode = "-1";
                }
                else
                {
                    account.IsVerified = true;
                    account.Verified = DateTime.UtcNow;
                    // account.VerificationToken = null;

                    _dbContext.TblUserMasters.Update(account);
                    _dbContext.SaveChanges();

                    _response.Message = "Verification successful, you can now login";
                    _response.Success = true;
                    _response.ErrorCode = "1";
                }
            }
            catch (Exception ex)
            {
                _response.Message = "Verification failed";
                _response.Success = false;
                _response.ErrorCode = "0";
            }

            return _response;
        }

        public ServiceResponse<bool> checkEmail(string email)
        {
            ServiceResponse<bool> _response = new ServiceResponse<bool>();
            try
            {
                if (_dbContext.TblUserMasters.Any(x => x.UserEmail == email))
                {
                    _response.ErrorCode = "-1";
                    _response.Message = "Email Already Registerd";
                    _response.Success = false;
                    _response.Model = false;
                }
                else
                {
                    _response.ErrorCode = "1";
                    _response.Message = "Email is Available.";
                    _response.Success = true;
                    _response.Model = true;
                }
            }
            catch (Exception ex)
            {
                _response.ErrorCode = "0";
                _response.Message = "something went wrong!!";
                _response.Success = false;
                _response.Model = false;
            }
            return _response;
        }

        public ServiceResponse<bool> ForgotPassword(string email, string origin)
        {
            ServiceResponse<bool> _response = new ServiceResponse<bool>();
            try
            {
                var account = _dbContext.TblUserMasters.SingleOrDefault(x => x.UserEmail == email && x.IsVerified.Value);

                // always return ok response to prevent email enumeration
                if (account == null)
                {
                    _response.ErrorCode = "-1";
                    _response.Message = "email not present into system!!";
                    _response.Success = false;
                    _response.Model = false;

                }
                else
                {
                    // create reset token that expires after 1 day
                    account.ResetToken = randomTokenString();
                    account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

                    _response.ErrorCode = "1";
                    _response.Message = "Please check your email for password reset instructions!!";
                    _response.Success = true;
                    _response.Model = true;
                    //// create reset token that expires after 1 day
                    //account.ResetToken = randomTokenString();
                    //account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

                    _dbContext.TblUserMasters.Update(account);
                    _dbContext.SaveChanges();

                    // send email
                    sendPasswordResetEmail(email, origin);
                }
            }
            catch (Exception ex)
            {
                _response.ErrorCode = "0";
                _response.Message = "something went wrong!!";
                _response.Success = false;
                _response.Model = false;
            }
            return _response;
        }

        public ServiceResponse<bool> ResetPassword(ResetPasswordRequest model)
        {
            ServiceResponse<bool> _response = new ServiceResponse<bool>();
            try
            {
                var account = _dbContext.TblUserMasters.SingleOrDefault(x =>
                x.ResetToken == model.Token &&
                x.ResetTokenExpires > DateTime.UtcNow);

                if (account == null)
                {
                    _response.ErrorCode = "-1";
                    _response.Message = "Invalid token !!";
                    _response.Success = false;
                    _response.Model = false;
                }
                else
                {
                    // update password and remove reset token
                    account.UserPassword = BC.HashPassword(model.Password);
                    account.ResetToken = null;
                    account.ResetTokenExpires = null;

                    _dbContext.TblUserMasters.Update(account);
                    _dbContext.SaveChanges();

                    _response.ErrorCode = "1";
                    _response.Message = "Password Reset !!";
                    _response.Success = true;
                    _response.Model = true;
                }
            }
            catch (Exception ex)
            {
                _response.ErrorCode = "0";
                _response.Message = "something went wrong!!";
                _response.Success = false;
                _response.Model = false;
            }

            return _response;
        }



        #endregion

        #region Private Methods

        private string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private string generateJwtToken(UserMasterVM account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = randomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        private void sendVerificationEmail(string verificationToken, string email, string origin, string fname)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/account/verify-email?token={verificationToken}";
                message = $@"<p><a style='color:blue' href=""{verifyUrl}"">Confirm my LoanCentral account</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                             <p><code>{verificationToken}</code></p>";
            }

            _emailService.Send(
                to: email,
                subject: "LoanCentral - Verify your email account",
                html: $@"<p>Hello {fname},</p>
                         <p>We’re excited you’ve joined LoanCentral.</p>
                         <p>As soon as you verify your email to confirm this is you, we can get started.</p>
                         <p>Just click the link below to verify your email:</p>
                         {message}
                         <p>Note: Link will expire in 48 hours</p>
                         <p>Having trouble? Contact our support desk <a href='#' style='color:blue'>here</a></p>
                         <p>LoanCentral team</p>   
                         "
            );
        }

        private void sendAlreadyRegisteredEmail(string email, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = $@"<p>If you don't know your password please visit the <a href=""{origin}/account/forgot-password"">forgot password</a> page.</p>";
            else
                message = "<p>If you don't know your password you can reset it via the <code>/accounts/forgot-password</code> api route.</p>";

            _emailService.Send(
                to: email,
                subject: "Sign-up Verification API - Email Already Registered",
                html: $@"<h4>Email Already Registered</h4>
                         <p>Your email <strong>{email}</strong> is already registered.</p>
                         {message}"
            );
        }

        private void loginAudit(string ip, int userid)
        {
            TblAuditLog auditLog = new TblAuditLog()
            {
                Ip = ip,
                UserId = userid,
                LoginDate = System.DateTime.Now
            };

            _dbContext.TblAuditLogs.Add(auditLog);
            _dbContext.SaveChanges();
        }

        private void sendPasswordResetEmail(string email, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/forgot-password";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>
                             <p>Having trouble? Contact our support desk <a href='#' style='color:blue'>here</a></p>
                             <p>LoanCentral team</p>";

            }
            else
            {
                message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                             <p><code></code></p>";
            }

            _emailService.Send(
                to: email,
                subject: "Sign-up Verification API - Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                         {message}"
            );
        }

        #endregion
    }
}
