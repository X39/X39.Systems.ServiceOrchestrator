using System.Security.Claims;

namespace X39.Util.AspNet.Authorize;

public delegate string GenerateJwtTokenDelegate(params Claim[] claims);