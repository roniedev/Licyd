import IndexPage from 'src/pages/IndexPage.vue';
import LoginPage from 'src/pages/LoginPage.vue';
import GrupoPage from 'src/pages/controleAcesso/grupo/pages/GrupoPage.vue';
import ModuloPage from 'src/pages/controleAcesso/modulo/pages/ModuloPage.vue';
import PaginaPage from 'src/pages/controleAcesso/pagina/pages/PaginaPage.vue';
import UsuarioPage from 'src/pages/controleAcesso/usuario/pages/UsuarioPage.vue';
import CategoriaItemPage from 'src/pages/cadastros/categoriaItem/pages/CategoriaItemPage.vue';
import ItemPage from 'src/pages/cadastros/item/pages/ItemPage.vue';
import ListaPrecoPage from 'src/pages/cadastros/listaPreco/pages/ListaPrecoPage.vue';
import MestreDetalhe from 'src/pages/componentes/MestreDetalhe.vue';

import { RouteRecordRaw } from 'vue-router';
import CadastroPage from 'src/pages/componentes/CadastroPage.vue';
import CardapioPage from 'src/pages/cardapio/pages/CardapioPage.vue';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        path: '',
        name: 'IndexPage',
        component: IndexPage,
      },
      {
        path: '/componentes/mestredetalhe',
        name: 'MestreDetalhe',
        component: MestreDetalhe,
      },
      {
        path: '/componentes/cadastro',
        name: 'CadastroPage',
        component: CadastroPage,
      },
      {
        path: '/controleacesso/modulo',
        name: 'ModuloPage',
        component: ModuloPage,
      },
      {
        path: '/controleacesso/pagina',
        name: 'PaginaPage',
        component: PaginaPage,
      },
      {
        path: '/controleacesso/usuario',
        name: 'UsuarioPage',
        component: UsuarioPage,
      },
      {
        path: '/controleacesso/grupo',
        name: 'GrupoPage',
        component: GrupoPage,
      },
      {
        path: '/cadastros/categoriaItem',
        name: 'CategoriaItemPage',
        component: CategoriaItemPage,
      },
      {
        path: '/cadastros/item',
        name: 'ItemPage',
        component: ItemPage,
      },
      {
        path: '/cadastros/listaPreco',
        name: 'ListaPrecoPage',
        component: ListaPrecoPage,
      },
      {
        path: '/cardapio/',
        name: 'CardapioPage',
        component: CardapioPage,
      },
    ],
  },
  {
    path: '/login',
    name: 'LoginPage',
    component: LoginPage,
  },
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue'),
  },
];

export default routes;
