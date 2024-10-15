﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MbaBlog.Data.Data;
using MbaBlog.Data.Domain;

namespace MbaBlog.WebApi.Helpers;

public class DbMigrationHelps
{
    public static async Task EnsureSeedData(WebApplication scope)
    {
        var service = scope.Services.CreateScope().ServiceProvider;
        await EnsureSeedData(service);
    }

    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var myBlogDbContext = scope.ServiceProvider.GetRequiredService<MbaBlogDbContext>();


        if (env.IsDevelopment())
        {
            await applicationDbContext.Database.MigrateAsync();

            await myBlogDbContext.Database.MigrateAsync();

            await EnsureSeedPosts(applicationDbContext, myBlogDbContext);
        }

    }

    public static async Task EnsureSeedPosts(ApplicationDbContext applicationDbContext, MbaBlogDbContext myBlogDbContext)
    {
        if (applicationDbContext.Users.Any())
        {
            return;
        }
        var autorId = Guid.NewGuid();
        var autorId01 = Guid.NewGuid();
        var adminId = Guid.NewGuid();

        var nameUser = "usuario@mbablog.com";
        var nameUser02 = "usuario02@mbablog.com";
        var nameUserAdmin = "admin@mbablog.com";

        var roleId = Guid.NewGuid();

        await applicationDbContext.Users.AddAsync(MockUser(autorId, nameUser));
        await applicationDbContext.Users.AddAsync(MockUser(autorId01, nameUser02));
        await applicationDbContext.Users.AddAsync(MockUser(adminId, nameUserAdmin));

        await applicationDbContext.Roles.AddAsync(MockRoler(roleId));

        await applicationDbContext.SaveChangesAsync();

        await applicationDbContext.Set<IdentityUserRole<string>>()
            .AddAsync(new IdentityUserRole<string> { RoleId = roleId.ToString(), UserId = adminId.ToString() });

        await applicationDbContext.SaveChangesAsync();

        if (myBlogDbContext.Posts.Any())
        {
            return;
        }

        var postId = Guid.NewGuid();
        var postId2 = Guid.NewGuid();
        var postId3 = Guid.NewGuid();
        var postId4 = Guid.NewGuid();
        var postId5 = Guid.NewGuid();
        var postId6 = Guid.NewGuid();
        var postId7 = Guid.NewGuid();
        var postId8 = Guid.NewGuid();

        await myBlogDbContext.Posts.AddAsync(MockPost(autorId));
        await myBlogDbContext.Posts.AddAsync(MockPostComComentario(autorId, postId));
        await myBlogDbContext.Posts.AddAsync(MockPostComComentario(autorId, postId2));
        await myBlogDbContext.Posts.AddAsync(MockPostComComentario(autorId, postId3));
        await myBlogDbContext.Posts.AddAsync(MockPostComComentario(autorId, postId4));
        await myBlogDbContext.Posts.AddAsync(MockPostComComentario(autorId01, postId5));
        await myBlogDbContext.Posts.AddAsync(MockPostComComentario(autorId01, postId6));
        await myBlogDbContext.Posts.AddAsync(MockPostComComentario(autorId01, postId7));
        await myBlogDbContext.Posts.AddAsync(MockPostComComentario(autorId01, postId8));
        await myBlogDbContext.Posts.AddAsync(MockPost(autorId01));

        await myBlogDbContext.SaveChangesAsync();
    }

    private static IdentityUser MockUser(Guid autorId, string name)
    {
        return new IdentityUser
        {
            Id = autorId.ToString(),
            UserName = name,
            NormalizedUserName = name.ToUpper(),
            Email = name,
            NormalizedEmail = name.ToUpper(),
            AccessFailedCount = 0,
            LockoutEnabled = false,
            PasswordHash = "AQAAAAIAAYagAAAAEIuDQN6KV6vlgvrhjP+t9BDGEUFtMdfUoa4qdf0YPxxhF8OZxKD8/YZODRrynoi5+w==",
            TwoFactorEnabled = false,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
    }


    private static IdentityRole MockRoler(Guid roleId)
    {
        return new IdentityRole
        {
            Id = roleId.ToString(),
            Name = "admin",
            NormalizedName = "ADMIN",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
        };
    }

    private static Post MockPost(Guid autorId)
    {
        Random random = new();
        string sufixo = random.Next().ToString();
        var titulo = "Novo Post - " + sufixo;

        return new Post
        {
            Id = Guid.NewGuid(),
            AutorId = autorId,
            Titulo = titulo,
            Texto = "Lorem ipsum dolor sit amet consectetur, adipiscing elit dapibus nunc eleifend euismod, " +
            "pharetra inceptos natoque nascetur. Sem maecenas vehicula justo mollis tellus mattis ac parturient, " +
            "mauris faucibus fames commodo dictumst eget imperdiet enim blandit, lectus fusce sociosqu morbi " +
            "interdum egestas dui. Nascetur bibendum metus congue mattis euismod ligula sollicitudin id aptent " +
            "magna eros rutrum ultricies, sem faucibus feugiat imperdiet fames consequat etiam sociosqu morbi " +
            "taciti iaculis. Arcu vitae curabitur rutrum ridiculus enim nam litora rhoncus mattis nisi morbi, " +
            "integer urna nisl phasellus nostra sagittis purus diam nec tellus, aptent feugiat mus tincidunt cursus " +
            "lectus fames sociis tristique netus. Nullam pulvinar purus luctus libero inceptos molestie cras dui, " +
            "arcu id donec risus nostra convallis ac, class tempus sapien metus nulla maecenas justo. " +
            "Phasellus feugiat id primis risus mi imperdiet natoque arcu himenaeos, " +
            "et purus non ultrices dignissim eleifend euismod aenean vehic",
            CriadoEm = DateTime.Now,
            ModificadoEm = DateTime.Now,
        };
    }

    private static Post MockPostComComentario(Guid autorId, Guid postId)
    {
        Random random = new();
        string sufixo = random.Next().ToString();
        var titulo = "Novo Post - " + sufixo;

        return new Post
        {
            Id = postId,
            AutorId = autorId,
            Titulo = titulo,
            Texto = "Lorem ipsum dolor sit amet consectetur, adipiscing elit dapibus nunc eleifend euismod, " +
            "pharetra inceptos natoque nascetur. Sem maecenas vehicula justo mollis tellus mattis ac parturient, " +
            "mauris faucibus fames commodo dictumst eget imperdiet enim blandit, lectus fusce sociosqu morbi " +
            "interdum egestas dui. Nascetur bibendum metus congue mattis euismod ligula sollicitudin id aptent " +
            "magna eros rutrum ultricies, sem faucibus feugiat imperdiet fames consequat etiam sociosqu morbi " +
            "taciti iaculis. Arcu vitae curabitur rutrum ridiculus enim nam litora rhoncus mattis nisi morbi, " +
            "integer urna nisl phasellus nostra sagittis purus diam nec tellus, aptent feugiat mus tincidunt cursus " +
            "lectus fames sociis tristique netus. Nullam pulvinar purus luctus libero inceptos molestie cras dui, " +
            "arcu id donec risus nostra convallis ac, class tempus sapien metus nulla maecenas justo. " +
            "Phasellus feugiat id primis risus mi imperdiet natoque arcu himenaeos, " +
            "et purus non ultrices dignissim eleifend euismod aenean vehic",
            CriadoEm = DateTime.Now,
            ModificadoEm = DateTime.Now,
            Comentarios = MockComentario(autorId, postId),
        };
    }

    private static List<ComentarioPost> MockComentario(Guid autorId, Guid postId)
    {
        var listCom = new List<ComentarioPost>();

        var com = new ComentarioPost
        {
            Id = Guid.NewGuid(),
            AutorId = autorId,
            Comentario = "Novissimo comentario pharetra inceptos natoque nascetur. Sem mollis tellus mattis ac parturient, " +
            "mauris faucibus fames commodo dictumst eget imperdiet enim blandit, lectus fusce sociosqu morbi " +
            "interdum egestas dui. Nascetur bibendum metus congue mattis euismod ligula sollicitudin id aptent ",
            PostId = postId
        };

        var com1 = new ComentarioPost
        {
            Id = Guid.NewGuid(),
            AutorId = autorId,
            Comentario = "Novo comentario pharetra inceptos natoque nascetur. Sem maecenas vehicula justo, " +
            "mauris faucibus fames commodo dictumst eget imperdiet enim blandit, lectus fusce sociosqu morbi " +
            "interdum egestas dui. Nascetur bibendum metus congue mattis euismod ligula sollicitudin id aptent ",
            PostId = postId
        };

        listCom.Add(com);
        listCom.Add(com1);

        return listCom;

    }
}