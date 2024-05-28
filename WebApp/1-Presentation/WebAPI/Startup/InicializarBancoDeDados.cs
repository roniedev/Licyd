//using Data.Contexts;
//using Domain.Entities;
//using Jump.BaseApp;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace WebAPI.Startup
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public class InicializarBancoDeDados(IServiceProvider serviceProvider) : IHostedService
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public async Task StartAsync(CancellationToken cancellationToken)
//        {
//            using var serviceScope = serviceProvider.CreateScope();
//            var services = serviceScope.ServiceProvider;
//            var context = services.GetRequiredService<AppDbContext>();

//            await ExecutarMigrations(serviceProvider);

//            await InserirModulo(context);
//            await InserirPaginas(context);
//            await InserirPaginaAcoes(context);
//            await InserirGrupo(context);
//            await InserirGrupoPaginas(context);
//            await InserirGrupoPaginaAcoes(context);
//            await InserirUsuario(services, context);
//        }

//        private static async Task ExecutarMigrations(IServiceProvider serviceProvider)
//        {
//            using var scope = serviceProvider.CreateScope();
//            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//            await context.Database.MigrateAsync();
//        }

//        private static async Task InserirModulo(AppDbContext context)
//        {
//            if (!context.Set<Modulo>().Any())
//            {
//                var modulo = new Modulo
//                {
//                    Id = 1,
//                    Nome = "Controle de acesso",
//                    StatusId = StatusEnum.Active,
//                    CreatedAt = DateTime.Now,
//                    CreatedBy = "Sistema",
//                    UpdatedAt = DateTime.Now,
//                    UpdatedBy = "Sistema"
//                };

//                await context.Set<Modulo>().AddAsync(modulo);
//            }
//        }

//        private static async Task InserirPaginas(AppDbContext context)
//        {
//            if (!context.Set<Pagina>().Any())
//            {
//                var paginas = new List<Pagina>
//                {
//                    new()
//                    {
//                        Id = 1,
//                        ModuloId = 1,
//                        Nome = "Módulos",
//                        IsVisivelNoMenu = true,
//                        Componente = "ModuloPage",
//                        ApiController = "Modulo",
//                        StatusId = StatusEnum.Active,
//                        CreatedAt = DateTime.Now,
//                        CreatedBy = "Sistema",
//                        UpdatedAt = DateTime.Now,
//                        UpdatedBy = "Sistema"
//                    },
//                    new()
//                    {
//                        Id = 2,
//                        ModuloId = 1,
//                        Nome = "Páginas",
//                        IsVisivelNoMenu = true,
//                        Componente = "PaginaPage",
//                        ApiController = "Pagina",
//                        StatusId = StatusEnum.Active,
//                        CreatedAt = DateTime.Now,
//                        CreatedBy = "Sistema", UpdatedAt =
//                        DateTime.Now, UpdatedBy = "Sistema"
//                    },
//                    new()
//                    {
//                        Id = 3,
//                        ModuloId = 1,
//                        Nome = "Grupos",
//                        IsVisivelNoMenu = true,
//                        Componente = "GrupoPage",
//                        ApiController = "Grupo",
//                        StatusId = StatusEnum.Active,
//                        CreatedAt = DateTime.Now,
//                        CreatedBy = "Sistema",
//                        UpdatedAt = DateTime.Now,
//                        UpdatedBy = "Sistema"
//                    },
//                    new()
//                    {
//                        Id = 4,
//                        ModuloId = 1,
//                        Nome = "Usuários",
//                        IsVisivelNoMenu = true,
//                        Componente = "UsuarioPage",
//                        ApiController = "Usuario",
//                        StatusId = StatusEnum.Active,
//                        CreatedAt = DateTime.Now,
//                        CreatedBy = "Sistema",
//                        UpdatedAt = DateTime.Now,
//                        UpdatedBy = "Sistema"
//                    }
//                };

//                await context.Set<Pagina>().AddRangeAsync(paginas);
//            }
//        }

//        private static async Task InserirPaginaAcoes(AppDbContext context)
//        {
//            if (!context.Set<PaginaAcao>().Any())
//            {
//                var paginaAcoes = new List<PaginaAcao>
//                {
//                    new () { Id = 1, PaginaId = 1, Codigo = "Activate", Nome = "Ativar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 2, PaginaId = 1, Codigo = "Update", Nome = "Atualizar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 3, PaginaId = 1, Codigo = "UpdateByExcel", Nome = "Atualizar por importação de planilha", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 4, PaginaId = 1, Codigo = "GetUpdateLayout", Nome = "Download da planilha de atualização de registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 5, PaginaId = 1, Codigo = "Delete", Nome = "Apagar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 6, PaginaId = 1, Codigo = "Export", Nome = "Exportar registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 7, PaginaId = 1, Codigo = "Inactivate", Nome = "Inativar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 8, PaginaId = 1, Codigo = "GetInactivateLayout", Nome = "Download da planilha de inativação de registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 9, PaginaId = 1, Codigo = "Insert", Nome = "Inserir um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 10, PaginaId = 1, Codigo = "InsertByImport", Nome = "Inserir por importação de planilha", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 11, PaginaId = 1, Codigo = "GetInsertLayout", Nome = "Download da planilha de inserir de registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 12, PaginaId = 1, Codigo = "GetById", Nome = "Listar um registro pelo identificador", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 13, PaginaId = 1, Codigo = "GetAll", Nome = "Listar todos os registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 14, PaginaId = 1, Codigo = "GetAllPaginated", Nome = "Listar todos os registros paginados", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },

//                    new () { Id = 15, PaginaId = 2, Codigo = "Activate", Nome = "Ativar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 16, PaginaId = 2, Codigo = "Update", Nome = "Atualizar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 17, PaginaId = 2, Codigo = "UpdateByExcel", Nome = "Atualizar por importação de planilha", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 18, PaginaId = 2, Codigo = "GetUpdateLayout", Nome = "Download da planilha de atualização de registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 19, PaginaId = 2, Codigo = "Delete", Nome = "Apagar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 20, PaginaId = 2, Codigo = "Export", Nome = "Exportar registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 21, PaginaId = 2, Codigo = "Inactivate", Nome = "Inativar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 22, PaginaId = 2, Codigo = "GetInactivateLayout", Nome = "Download da planilha de inativação de registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 23, PaginaId = 2, Codigo = "Insert", Nome = "Inserir um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 24, PaginaId = 2, Codigo = "InsertByImport", Nome = "Inserir por importação de planilha", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 25, PaginaId = 2, Codigo = "GetInsertLayout", Nome = "Download da planilha de inserir de registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 26, PaginaId = 2, Codigo = "GetById", Nome = "Listar um registro pelo identificador", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 27, PaginaId = 2, Codigo = "GetAll", Nome = "Listar todos os registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 28, PaginaId = 2, Codigo = "GetAllPaginated", Nome = "Listar todos os registros paginados", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },

//                    new () { Id = 29, PaginaId = 3, Codigo = "Activate", Nome = "Ativar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 30, PaginaId = 3, Codigo = "Update", Nome = "Atualizar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 31, PaginaId = 3, Codigo = "UpdateByExcel", Nome = "Atualizar por importação de planilha", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 32, PaginaId = 3, Codigo = "GetUpdateLayout", Nome = "Download da planilha de atualização de registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 33, PaginaId = 3, Codigo = "Delete", Nome = "Apagar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 34, PaginaId = 3, Codigo = "Export", Nome = "Exportar registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 35, PaginaId = 3, Codigo = "Inactivate", Nome = "Inativar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 36, PaginaId = 3, Codigo = "GetInactivateLayout", Nome = "Download da planilha de inativação de registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 37, PaginaId = 3, Codigo = "Insert", Nome = "Inserir um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 38, PaginaId = 3, Codigo = "InsertByImport", Nome = "Inserir por importação de planilha", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 39, PaginaId = 3, Codigo = "GetInsertLayout", Nome = "Download da planilha de inserir de registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 40, PaginaId = 3, Codigo = "GetById", Nome = "Listar um registro pelo identificador", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 41, PaginaId = 3, Codigo = "GetAll", Nome = "Listar todos os registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 42, PaginaId = 3, Codigo = "GetAllPaginated", Nome = "Listar todos os registros paginados", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },

//                    new () { Id = 43, PaginaId = 4, Codigo = "Activate", Nome = "Ativar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 44, PaginaId = 4, Codigo = "Update", Nome = "Atualizar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 45, PaginaId = 4, Codigo = "UpdateByExcel", Nome = "Atualizar por importação de planilha", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 46, PaginaId = 4, Codigo = "GetUpdateLayout", Nome = "Download da planilha de atualização de registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 47, PaginaId = 4, Codigo = "Delete", Nome = "Apagar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 48, PaginaId = 4, Codigo = "Export", Nome = "Exportar registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 49, PaginaId = 4, Codigo = "Inactivate", Nome = "Inativar um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 50, PaginaId = 4, Codigo = "GetInactivateLayout", Nome = "Download da planilha de inativação de registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 51, PaginaId = 4, Codigo = "Insert", Nome = "Inserir um registro", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 52, PaginaId = 4, Codigo = "InsertByImport", Nome = "Inserir por importação de planilha", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 53, PaginaId = 4, Codigo = "GetInsertLayout", Nome = "Download da planilha de inserir de registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 54, PaginaId = 4, Codigo = "GetById", Nome = "Listar um registro pelo identificador", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 55, PaginaId = 4, Codigo = "GetAll", Nome = "Listar todos os registros", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new () { Id = 56, PaginaId = 4, Codigo = "GetAllPaginated", Nome = "Listar todos os registros paginados", CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" }
//                };

//                await context.Set<PaginaAcao>().AddRangeAsync(paginaAcoes);
//            }
//        }

//        private static async Task InserirGrupo(AppDbContext context)
//        {
//            if (!context.Set<Grupo>().Any())
//            {
//                var grupo = new Grupo
//                {
//                    Id = 1,
//                    Nome = "Administradores",
//                    StatusId = StatusEnum.Active,
//                    CreatedAt = DateTime.Now,
//                    CreatedBy = "Sistema",
//                    UpdatedAt = DateTime.Now,
//                    UpdatedBy = "Sistema"
//                };

//                await context.Set<Grupo>().AddAsync(grupo);
//            }
//        }

//        private static async Task InserirGrupoPaginas(AppDbContext context)
//        {
//            if (!context.Set<GrupoPagina>().Any())
//            {
//                var grupoPaginas = new List<GrupoPagina>
//                {
//                    new() { Id = 1, GrupoId = 1, PaginaId = 1, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 2, GrupoId = 1, PaginaId = 2, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 3, GrupoId = 1, PaginaId = 3, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 4, GrupoId = 1, PaginaId = 4, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" }
//                };

//                await context.Set<GrupoPagina>().AddRangeAsync(grupoPaginas);
//            }
//        }

//        private static async Task InserirGrupoPaginaAcoes(AppDbContext context)
//        {
//            if (!context.Set<GrupoPaginaAcao>().Any())
//            {
//                var grupoPaginas = new List<GrupoPaginaAcao>
//                {
//                    new() { Id = 1, GrupoPaginaId = 1, PaginaAcaoId = 1, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 2, GrupoPaginaId = 1, PaginaAcaoId = 2, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 3, GrupoPaginaId = 1, PaginaAcaoId = 3, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 4, GrupoPaginaId = 1, PaginaAcaoId = 4, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 5, GrupoPaginaId = 1, PaginaAcaoId = 5, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 6, GrupoPaginaId = 1, PaginaAcaoId = 6, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 7, GrupoPaginaId = 1, PaginaAcaoId = 7, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 8, GrupoPaginaId = 1, PaginaAcaoId = 8, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 9, GrupoPaginaId = 1, PaginaAcaoId = 9, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 10, GrupoPaginaId = 1, PaginaAcaoId = 10, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 11, GrupoPaginaId = 1, PaginaAcaoId = 11, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 12, GrupoPaginaId = 1, PaginaAcaoId = 12, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 13, GrupoPaginaId = 1, PaginaAcaoId = 13, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 14, GrupoPaginaId = 1, PaginaAcaoId = 14, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },

//                    new() { Id = 15, GrupoPaginaId = 2, PaginaAcaoId = 15, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 16, GrupoPaginaId = 2, PaginaAcaoId = 16, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 17, GrupoPaginaId = 2, PaginaAcaoId = 17, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 18, GrupoPaginaId = 2, PaginaAcaoId = 18, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 19, GrupoPaginaId = 2, PaginaAcaoId = 19, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 20, GrupoPaginaId = 2, PaginaAcaoId = 20, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 21, GrupoPaginaId = 2, PaginaAcaoId = 21, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 22, GrupoPaginaId = 2, PaginaAcaoId = 22, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 23, GrupoPaginaId = 2, PaginaAcaoId = 23, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 24, GrupoPaginaId = 2, PaginaAcaoId = 24, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 25, GrupoPaginaId = 2, PaginaAcaoId = 25, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 26, GrupoPaginaId = 2, PaginaAcaoId = 26, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 27, GrupoPaginaId = 2, PaginaAcaoId = 27, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 28, GrupoPaginaId = 2, PaginaAcaoId = 28, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },

//                    new() { Id = 29, GrupoPaginaId = 3, PaginaAcaoId = 29, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 30, GrupoPaginaId = 3, PaginaAcaoId = 30, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 31, GrupoPaginaId = 3, PaginaAcaoId = 31, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 32, GrupoPaginaId = 3, PaginaAcaoId = 32, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 33, GrupoPaginaId = 3, PaginaAcaoId = 33, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 34, GrupoPaginaId = 3, PaginaAcaoId = 34, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 35, GrupoPaginaId = 3, PaginaAcaoId = 35, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 36, GrupoPaginaId = 3, PaginaAcaoId = 36, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 37, GrupoPaginaId = 3, PaginaAcaoId = 37, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 38, GrupoPaginaId = 3, PaginaAcaoId = 38, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 39, GrupoPaginaId = 3, PaginaAcaoId = 39, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 40, GrupoPaginaId = 3, PaginaAcaoId = 40, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 41, GrupoPaginaId = 3, PaginaAcaoId = 41, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 42, GrupoPaginaId = 3, PaginaAcaoId = 42, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },

//                    new() { Id = 43, GrupoPaginaId = 4, PaginaAcaoId = 43, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 44, GrupoPaginaId = 4, PaginaAcaoId = 44, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 45, GrupoPaginaId = 4, PaginaAcaoId = 45, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 46, GrupoPaginaId = 4, PaginaAcaoId = 46, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 47, GrupoPaginaId = 4, PaginaAcaoId = 47, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 48, GrupoPaginaId = 4, PaginaAcaoId = 48, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 49, GrupoPaginaId = 4, PaginaAcaoId = 49, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 50, GrupoPaginaId = 4, PaginaAcaoId = 50, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 51, GrupoPaginaId = 4, PaginaAcaoId = 51, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 52, GrupoPaginaId = 4, PaginaAcaoId = 52, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 53, GrupoPaginaId = 4, PaginaAcaoId = 53, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 54, GrupoPaginaId = 4, PaginaAcaoId = 54, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 55, GrupoPaginaId = 4, PaginaAcaoId = 55, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" },
//                    new() { Id = 56, GrupoPaginaId = 4, PaginaAcaoId = 56, CreatedAt = DateTime.Now, CreatedBy = "Sistema", UpdatedAt = DateTime.Now, UpdatedBy = "Sistema" }
//                };

//                await context.Set<GrupoPaginaAcao>().AddRangeAsync(grupoPaginas);
//            }
//        }

//        private static async Task InserirUsuario(IServiceProvider services, AppDbContext context)
//        {
//            if (!context.Users.Any())
//            {
//                var userStore = services.GetRequiredService<IUserStore<Usuario>>();
//                var userManager = services.GetRequiredService<UserManager<Usuario>>();

//                var usuario = new Usuario
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Nome = "Administrador",
//                    UserName = "admin",
//                    Email = "ronie.nogueira@outlook.com",
//                    StatusId = StatusEnum.Active,
//                    CreatedAt = DateTime.Now,
//                    CreatedBy = "Sistema",
//                    UpdatedAt = DateTime.Now,
//                    UpdatedBy = "Sistema"
//                };

//                await userStore.SetUserNameAsync(usuario, usuario.UserName, CancellationToken.None);
//                await userManager.CreateAsync(usuario, "Admin@1234");

//                var grupoUsuario = new GrupoUsuario
//                {
//                    Id = 1,
//                    GrupoId = 1,
//                    UsuarioId = usuario.Id,
//                    CreatedAt = DateTime.Now,
//                    CreatedBy = "Sistema",
//                    UpdatedAt = DateTime.Now,
//                    UpdatedBy = "Sistema"
//                };

//                context.Set<GrupoUsuario>().Add(grupoUsuario);
//                context.SaveChanges();
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            return Task.CompletedTask;
//        }
//    }
//}
