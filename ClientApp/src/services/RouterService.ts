import { IUsuarioModulo } from 'src/components/menu/interfaces/IUsuarioModulo';
import { Router } from 'vue-router';

class RouterService {
  private readonly url: string;
  private readonly router: Router;

  constructor(url: string, router: Router) {
    this.url = url;
    this.router = router;
  }

  removerRotasSemPermisao = (modulos: IUsuarioModulo[]) => {
    const rotasAplicacao =
      this.router
        .getRoutes()
        .find((x) => x.name === 'routesApp')
        ?.children.map((x) => x.name?.toString()) ?? [];

    const rotasUsuario = this.getRotasUsuario(modulos);

    if (rotasUsuario) {
      const rotasParaRemover = rotasAplicacao.filter((item) => {
        if (item) rotasUsuario.includes(item);
      });

      rotasParaRemover
        .filter((item) => item !== 'IndexPage')
        .forEach((x) => {
          if (x) this.router.removeRoute(x);
        });
    }
  };

  getRotasUsuario = (usuarioModulos: IUsuarioModulo[]) => {
    let paginas: string[] = [];

    usuarioModulos.forEach((modulo) => {
      paginas = [
        ...paginas,
        ...modulo.paginas.map((pagina) => pagina.componente),
      ];

      if (modulo.subModulos?.length > 0)
        paginas = [...paginas, ...this.getRotasUsuario(modulo.subModulos)];
    });

    return paginas;
  };
}

export default RouterService;
