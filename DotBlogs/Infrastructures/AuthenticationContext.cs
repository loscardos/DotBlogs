using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotBlogs.Infrastructures;

public class AuthenticationContext(DbContextOptions<AuthenticationContext> options) : IdentityDbContext(options);