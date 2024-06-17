import { IRoute } from 'src/interfaces/IRoute';
import { Router } from 'vue-router';

class RouterService {
  private readonly url: string;
  private readonly router: Router;

  constructor(url: string, router: Router) {
    this.url = url;
    this.router = router;
  }

  removerRotasSemPermisao = (rotas: Array<IRoute>) => {
    const nomeRotasAplicacao =
      this.router
        .getRoutes()
        .find((x) => x.name === 'routesApp')
        ?.children.map((x) => x.name?.toString()) ?? [];

    const nomeDasRotasDoUsuario = rotas.map((x) => x.component);

    if (nomeDasRotasDoUsuario) {
      const rotasParaRemover = nomeRotasAplicacao.filter((item) => {
        if (item) nomeDasRotasDoUsuario.includes(item);
      });

      rotasParaRemover
        .filter((item) => item !== 'IndexPage' && item !== 'ComponentesPage')
        .forEach((x) => {
          if (x) this.router.removeRoute(x);
        });
    }
  };
}

export default RouterService;
