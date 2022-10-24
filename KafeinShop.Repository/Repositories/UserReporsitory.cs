//using KafeinShop.Core.Model;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace KafeinShop.Repository.Repositories
//{
//    public class UserRepository : GenericRepository<User>, IUserRepository
//    {
//        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
//        private readonly UserManager<User> _userManager;
//        private readonly RoleManager<Role> _roleManager;
//        private readonly SignInManager<User> _signInManager;
//        private readonly JWTSettings _jwtSettings;

//        public UserRepository(ApplicationDbContext dbContext,
//            IUserRefreshTokenRepository userRefreshTokenRepository,
//            UserManager<User> userManager,
//            SignInManager<User> signInManager,
//            IOptions<JWTSettings> jwtSettings, RoleManager<Role> roleManager) : base(dbContext)
//        {
//            _userRefreshTokenRepository = userRefreshTokenRepository;
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _roleManager = roleManager;
//            _jwtSettings = jwtSettings.Value;
//        }

//        public async Task<bool> IsEmailUnique(string email)
//        {
//            return await _userManager.FindByEmailAsync(email) == null;
//        }

//        public async Task<bool> IsUserNameUnique(string userName)
//        {
//            return await _userManager.FindByNameAsync(userName) == null;
//        }

//        public async Task<bool> IsEmailConfirmed(string email)
//        {
//            return await Get(x => x.NormalizedEmail == _userManager.NormalizeEmail(email),
//                null,
//                user => new User
//                {
//                    Id = user.Id,
//                    EmailConfirmed = user.EmailConfirmed
//                }) != null;
//        }

//        public async Task<bool> AddToRole(User user, string roleName)
//        {
//            var result = await _userManager.AddToRoleAsync(user, roleName);
//            return result.Succeeded
//                ? true
//                : throw new ValidationException(result.Errors);
//        }

//        public new async Task<bool> AddAsync(User user)
//        {
//            var isEmailUnique = await IsEmailUnique(user.Email);

//            if (!isEmailUnique)
//                throw new ValidationException(new Dictionary<string, string[]>
//                { { "Email", new[] { "Email must be Unique" } } });

//            var isUserNameExist = await IsUserNameUnique(user.UserName);
//            if (!isUserNameExist)
//                throw new ValidationException(new Dictionary<string, string[]>
//                { { "UserName", new[] { "UserName must be Unique" } } });

//            var result = await _userManager.CreateAsync(user, user.PasswordHash);
//            return result.Succeeded
//                ? true
//                : throw new ValidationException(result.Errors);
//        }

//        public new async Task<bool> UpdateAsync(User user)
//        {
//            var result = await _userManager.UpdateAsync(user);
//            return result.Succeeded
//                ? true
//                : throw new ValidationException(result.Errors);
//        }

//        public new async Task<bool> DeleteAsync(User user)
//        {
//            var result = await _userManager.DeleteAsync(user);
//            return result.Succeeded
//                ? true
//                : throw new ValidationException(result.Errors);
//        }

//        public Task<User?> GetByIdAsync(string roleId)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<IList<string>> GetUserRolesByUser(User user)
//        {
//            return await _userManager.GetRolesAsync(user);
//        }

//        public async Task<User?> GetActiveUserById(string userId)
//        {
//            return await Get(filter: x => x.Id == userId && !x.IsDeleted, select: user => new User
//            {
//                Id = user.Id,
//                UserName = user.UserName,
//                Email = user.Email
//            });
//        }

//        public async Task<User?> GetActiveUserByIdWithInclude(string userId)
//        {
//            return await Get(x => x.Id == userId && !x.IsDeleted,
//                source => source
//                    .Include(x => x.UserGroups),
//            user => new User
//            {
//                Id = user.Id,
//                UserName = user.UserName,
//                Email = user.Email,
//                UserGroups = user.UserGroups
//            });
//        }

//        public async Task<bool> IsUserInRole(User user, string roleName)
//        {
//            return await _userManager.IsInRoleAsync(user, roleName);
//        }

//        public async Task<AuthenticationResponseDto> AuthenticateAsync(string email, string password)
//        {
//            var user = await Get(x => x.NormalizedEmail == _userManager.NormalizeEmail(email) && !x.IsDeleted, null,
//                user => new User
//                {
//                    Id = user.Id,
//                    UserName = user.UserName,
//                    Email = user.Email,
//                });

//            if (user == null)
//                throw new ApiException("No Active account found for given credentials");

//            var loginResult = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
//            if (!loginResult.Succeeded)
//                throw new ApiException("No Active account found for given credentials");

//            /*
//            if (!user.EmailConfirmed)
//                throw new ApiException($"Account Not Confirmed for '{request.Email}'");
//            */

//            var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
//            var jwtSecurityToken = GenerateJwtToken(user, userRoles);
//            var response = new AuthenticationResponseDto
//            {
//                Id = user.Id,
//                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
//                UserName = user.UserName,
//                FirstName = user.FirstName,
//                LastName = user.LastName,
//                Email = user.Email,
//                Roles = userRoles,
//                IsVerified = user.EmailConfirmed,
//            };
//            var refreshToken = GenerateRefreshToken();
//            response.RefreshToken = refreshToken.Token;

//            await _userRefreshTokenRepository.AddAsync(new UserRefreshToken
//            {
//                UserId = user.Id,
//                RefreshToken = refreshToken.Token,
//                RefreshTokenExpiryTime = refreshToken.Expires
//            });
//            return response;
//        }

//        public async Task<AuthenticationResponseDto> RefreshTokenAsync(string userId, string token)
//        {
//            var userRefreshToken = await _userRefreshTokenRepository.Get(x => x.UserId == userId &&
//                                                                              x.RefreshToken == token &&
//                                                                              x.RefreshTokenExpiryTime > DateTime.Now);
//            if (userRefreshToken == null)
//                throw new ApiException("No Token found for given credentials.");

//            var user = await _userManager.FindByIdAsync(userId);
//            if (user == null)
//                throw new ApiException("No Active Account found for given credentials.");

//            var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
//            var jwtSecurityToken = GenerateJwtToken(user, userRoles);
//            var response = new AuthenticationResponseDto
//            {
//                Id = user.Id,
//                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
//                UserName = user.UserName,
//                FirstName = user.FirstName,
//                LastName = user.LastName,
//                Email = user.Email,
//                Roles = userRoles,
//                IsVerified = user.EmailConfirmed,
//            };
//            var refreshToken = GenerateRefreshToken();
//            response.RefreshToken = refreshToken.Token;

//            await _userRefreshTokenRepository.AddAsync(new UserRefreshToken
//            {
//                UserId = user.Id,
//                RefreshToken = refreshToken.Token,
//                RefreshTokenExpiryTime = refreshToken.Expires
//            });
//            return response;
//        }

//        public async Task<bool> LogoutAsync(string email, string token)
//        {
//            var user = await _userManager.FindByEmailAsync(email);
//            if (user == null)
//                return false;
//            var refreshToken = await _userRefreshTokenRepository.Get(x => x.UserId == user.Id &&
//                                                                          x.RefreshToken == token);
//            if (refreshToken == null)
//                return true;
//            _userRefreshTokenRepository.DeleteAsync(refreshToken);
//            return true;
//        }

//        public async Task<bool> ConfirmEmailAsync(string userId, string code)
//        {
//            var user = await _userManager.FindByIdAsync(userId);
//            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
//            var result = await _userManager.ConfirmEmailAsync(user, code);
//            return result.Succeeded;
//        }


//        #region Private Methods

//        private JwtSecurityToken GenerateJwtToken(User user, IEnumerable<string> userRoles)
//        {
//            var roleClaims = userRoles.Select(t => new Claim("roles", t)).ToList();
//            var claims = new[]
//                {
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                new Claim(JwtRegisteredClaimNames.Email, user.Email),
//                new Claim("id", user.Id),
//            }
//                .Union(roleClaims);

//            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
//            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

//            return new JwtSecurityToken(
//                issuer: _jwtSettings.Issuer,
//                audience: _jwtSettings.Audience,
//                claims: claims,
//                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
//                signingCredentials: signingCredentials);
//        }

//        private string RandomTokenString()
//        {
//            var randomBytes = new byte[64];
//            using var rng = RandomNumberGenerator.Create();
//            rng.GetBytes(randomBytes);
//            return Convert.ToBase64String(randomBytes);
//        }

//        private RefreshTokenDto GenerateRefreshToken()
//        {
//            return new RefreshTokenDto
//            {
//                Token = RandomTokenString(),
//                Expires = DateTime.UtcNow.AddDays(10)
//            };
//        }

//        #endregion
//    }
//}
