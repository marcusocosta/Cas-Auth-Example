# Exemplo de utilização do CAS para autenticação e autorização

No exemplo utilizei o package [AspNet.Security.CAS](https://github.com/IUCrimson/AspNet.Security.CAS/tree/master/src/AspNetCore.Security.CAS)

**Startup.cs: ConfigureServices**

```csharp
    public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/login");
                    options.AccessDeniedPath = new PathString("/access-denied");
                    options.Cookie = new CookieBuilder
                    {
                        Name = ".AspNetCore.CasSample"
                    };
                })
                .AddCAS(options =>
                {
                    options.CasServerUrlBase = Configuration["CasBaseUrl"];
                    options.CallbackPath = new PathString("/api/signin");
                    options.TicketValidator = new Cas2TicketValidator();
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                });
                                                                  
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }
```

**Startup.cs: Configure**
```
    app.UseAuthentication();
```

Implementar a interface ICasTicketValidator para customizar a validação do ticket;