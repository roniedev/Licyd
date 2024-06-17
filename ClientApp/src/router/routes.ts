import CarteiraSetorPage from 'src/pages/cadastros/carteiraSetor/pages/CarteiraSetorPage.vue';
import SedePage from 'src/pages/cadastros/sede/pages/SedePage.vue';
import SetorPage from 'src/pages/cadastros/setor/pages/SetorPage.vue';
import SetorCadastroLxPage from 'src/pages/cadastros/setorCadastroLX/pages/SetorCadastroLxPage.vue';
import SetorClientesPage from 'src/pages/cadastros/setorClientes/pages/setorClientesPage.vue';
import MultiploAcessoRelatorio from 'src/pages/relatorios/pages/MultiploAcessoRelatorio.vue';
import { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'routesApp',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        path: '/',
        name: 'IndexPage',
        component: () => import('pages/IndexPage.vue'),
      },
      {
        path: '/NivelHierarquico',
        name: 'NivelHierarquicoIndex',
        component: () => import('pages/NivelHierarquicoIndex.vue'),
      },
      {
        path: '/CarteiraSetor',
        name: 'CarteiraSetorPage',
        component: CarteiraSetorPage,
      },
      {
        path: '/NivelHierarquico/Cadastro/:id?',
        name: 'NivelHierarquicoForm',
        component: () => import('pages/NivelHierarquicoForm.vue'),
      },
      {
        path: '/Divisao',
        name: 'DivisaoIndex',
        component: () => import('pages/DivisaoIndex.vue'),
      },
      {
        path: '/Divisao/Cadastro/:id?',
        name: 'DivisaoForm',
        component: () => import('pages/DivisaoForm.vue'),
      },
      {
        path: '/Subdivisao',
        name: 'SubdivisaoIndex',
        component: () => import('pages/SubdivisaoIndex.vue'),
      },
      {
        path: '/Subdivisao/Cadastro/:id?',
        name: 'SubdivisaoForm',
        component: () => import('pages/SubdivisaoForm.vue'),
      },
      {
        path: '/DivisaoTipoPedido',
        name: 'DivisaoTipoPedidoIndex',
        component: () => import('pages/DivisaoTipoPedidoIndex.vue'),
      },
      {
        path: '/DivisaoTipoPedido/Cadastro/:id?',
        name: 'DivisaoTipoPedidoForm',
        component: () => import('pages/DivisaoTipoPedidoForm.vue'),
      },
      {
        path: '/DivisaoSubdivisaoClasseItem',
        name: 'DivisaoSubdivisaoClasseItemIndex',
        component: () => import('pages/DivisaoSubdivisaoClasseItemIndex.vue'),
      },
      {
        path: '/DivisaoSubdivisaoClasseItem/Cadastro/:id?',
        name: 'DivisaoSubdivisaoClasseItemForm',
        component: () => import('pages/DivisaoSubdivisaoClasseItemForm.vue'),
      },
      // {
      //   path: '/Setor',
      //   name: 'SetorIndex',
      //   component: () => import('pages/SetorIndex.vue'),
      // },
      // {
      //   path: '/Setor/Cadastro/:id?',
      //   name: 'SetorForm',
      //   component: () => import('pages/SetorForm.vue'),
      // },
      // {
      //   path: '/SetorCadastroLX',
      //   name: 'SetorCadastroLxIndex',
      //   component: () => import('pages/SetorCadastroLxIndex.vue'),
      // },
      // {
      //   path: '/SetorCadastroLX/Cadastro/:id?',
      //   name: 'SetorCadastroLxForm',
      //   component: () => import('pages/SetorCadastroLxForm.vue'),
      // },
      // {
      //   path: '/SetorClientes',
      //   name: 'SetorClientesIndex',
      //   component: () => import('pages/SetorClientesIndex.vue'),
      // },
      // {
      //   path: '/SetorClientes/Cadastro/:id?',
      //   name: 'SetorClientesForm',
      //   component: () => import('pages/SetorClientesForm.vue'),
      // },
      {
        path: '/Abatimento',
        name: 'AbatimentoIndex',
        component: () => import('pages/AbatimentoIndex.vue'),
      },
      {
        path: '/Abatimento/Cadastro/:id?',
        name: 'AbatimentoForm',
        component: () => import('pages/AbatimentoForm.vue'),
      },
      {
        path: '/PedidoCancelado',
        name: 'PedidoCanceladoIndex',
        component: () => import('pages/PedidoCanceladoIndex.vue'),
      },
      {
        path: '/PedidoCancelado/Cadastro/:id?',
        name: 'PedidoCanceladoForm',
        component: () => import('pages/PedidoCanceladoForm.vue'),
      },
      {
        path: '/PedidoEnviadoCliente',
        name: 'PedidoEnviadoClienteIndex',
        component: () => import('pages/PedidoEnviadoClienteIndex.vue'),
      },
      {
        path: '/PedidoEnviadoCliente/Cadastro/:id?',
        name: 'PedidoEnviadoClienteForm',
        component: () => import('pages/PedidoEnviadoClienteForm.vue'),
      },
      {
        path: '/MotivoAbatimento',
        name: 'MotivoAbatimentoIndex',
        component: () => import('pages/MotivoAbatimentoIndex.vue'),
      },
      {
        path: '/MotivoAbatimento/Cadastro/:id?',
        name: 'MotivoAbatimentoForm',
        component: () => import('pages/MotivoAbatimentoForm.vue'),
      },
      {
        path: '/TipoDesconto',
        name: 'TipoDescontoIndex',
        component: () => import('pages/TipoDescontoIndex.vue'),
      },
      {
        path: '/TipoDesconto/Cadastro/:id?',
        name: 'TipoDescontoForm',
        component: () => import('pages/TipoDescontoForm.vue'),
      },
      {
        path: '/MotivoAfastamento',
        name: 'MotivoAfastamentoIndex',
        component: () => import('pages/MotivoAfastamentoIndex.vue'),
      },
      {
        path: '/MotivoAfastamento/Cadastro/:id?',
        name: 'MotivoAfastamentoForm',
        component: () => import('pages/MotivoAfastamentoForm.vue'),
      },
      {
        path: '/Afastamento',
        name: 'AfastamentoIndex',
        component: () => import('pages/AfastamentoIndex.vue'),
      },
      {
        path: '/Afastamento/Cadastro/:id?',
        name: 'AfastamentoForm',
        component: () => import('pages/AfastamentoForm.vue'),
      },
      {
        path: '/ObjetivoPeriodoMesDivisao',
        name: 'ObjetivoPeriodoMesDivisaoIndex',
        component: () => import('pages/ObjetivoPeriodoMesDivisaoIndex.vue'),
      },
      {
        path: '/ObjetivoPeriodoMesDivisaoForm/Cadastro/:id?',
        name: 'ObjetivoPeriodoMesDivisaoForm',
        component: () => import('pages/ObjetivoPeriodoMesDivisaoForm.vue'),
      },
      {
        path: '/ObjetivoPeriodoMesSubdivisao',
        name: 'ObjetivoPeriodoMesSubdivisaoIndex',
        component: () => import('pages/ObjetivoPeriodoMesSubdivisaoIndex.vue'),
      },
      {
        path: '/ObjetivoPeriodoMesSubdivisao/Cadastro/:id?',
        name: 'ObjetivoPeriodoMesSubdivisaoForm',
        component: () => import('pages/ObjetivoPeriodoMesSubdivisaoForm.vue'),
      },
      {
        path: '/ObjetivoValorGeral',
        name: 'ObjetivoValorGeralIndex',
        component: () => import('pages/ObjetivoValorGeralIndex.vue'),
      },
      {
        path: '/ObjetivoValorGeral/Cadastro/:id?',
        name: 'ObjetivoValorGeralForm',
        component: () => import('pages/ObjetivoValorGeralForm.vue'),
      },
      {
        path: '/ObjetivoValorDivisao',
        name: 'ObjetivoValorDivisaoIndex',
        component: () => import('pages/ObjetivoValorDivisaoIndex.vue'),
      },
      {
        path: '/ObjetivoValorDivisao/Cadastro/:id?',
        name: 'ObjetivoValorDivisaoForm',
        component: () => import('pages/ObjetivoValorDivisaoForm.vue'),
      },
      {
        path: '/ObjetivoValorSubdivisao',
        name: 'ObjetivoValorSubdivisaoIndex',
        component: () => import('pages/ObjetivoValorSubdivisaoIndex.vue'),
      },
      {
        path: '/ObjetivoValorSubdivisao/Cadastro/:id?',
        name: 'ObjetivoValorSubdivisaoForm',
        component: () => import('pages/ObjetivoValorSubdivisaoForm.vue'),
      },
      {
        path: '/ObjetivoValorSetor',
        name: 'ObjetivoValorSetorIndex',
        component: () => import('pages/ObjetivoValorSetorIndex.vue'),
      },
      {
        path: '/ObjetivoValorSetor/Cadastro/:id?',
        name: 'ObjetivoValorSetorForm',
        component: () => import('pages/ObjetivoValorSetorForm.vue'),
      },
      {
        path: '/ObjetivoValorCliente',
        name: 'ObjetivoValorClienteIndex',
        component: () => import('pages/ObjetivoValorClienteIndex.vue'),
      },
      {
        path: '/ObjetivoValorCliente/Cadastro/:id?',
        name: 'ObjetivoValorClienteForm',
        component: () => import('pages/ObjetivoValorClienteForm.vue'),
      },
      {
        path: '/ObjetivoPercentualExcecao',
        name: 'ObjetivoPercentualExcecaoIndex',
        component: () => import('pages/ObjetivoPercentualExcecaoIndex.vue'),
      },
      {
        path: '/ObjetivoPercentualExcecao/Cadastro/:id?',
        name: 'ObjetivoPercentualExcecaoForm',
        component: () => import('pages/ObjetivoPercentualExcecaoForm.vue'),
      },
      {
        path: '/ObjetivoPercentualPadrao',
        name: 'ObjetivoPercentualPadraoIndex',
        component: () => import('pages/ObjetivoPercentualPadraoIndex.vue'),
      },
      {
        path: '/ObjetivoPercentualPadrao/Cadastro/:id?',
        name: 'ObjetivoPercentualPadraoForm',
        component: () => import('pages/ObjetivoPercentualPadraoForm.vue'),
      },
      {
        path: '/AssociacaoExcecaoSetor',
        name: 'AssociacaoExcecaoSetorIndex',
        component: () => import('pages/AssociacaoExcecaoSetorIndex.vue'),
      },
      {
        path: '/AssociacaoExcecaoSetor/Cadastro/:id?',
        name: 'AssociacaoExcecaoSetorForm',
        component: () => import('pages/AssociacaoExcecaoSetorForm.vue'),
      },
      {
        path: '/UsuarioDivisao',
        name: 'UsuarioDivisaoIndex',
        component: () => import('pages/UsuarioDivisaoIndex.vue'),
      },
      {
        path: '/UsuarioDivisao/Cadastro/:id?',
        name: 'UsuarioDivisaoForm',
        component: () => import('pages/UsuarioDivisaoForm.vue'),
      },
      {
        path: '/Setor',
        name: 'SetorPage',
        component: SetorPage,
      },
      {
        path: '/Sede',
        name: 'SedePage',
        component: SedePage,
      },
      {
        path: '/SetorCadastroLX',
        name: 'SetorCadastroLxPage',
        component: SetorCadastroLxPage,
      },
      {
        path: '/SetorClientes',
        name: 'SetorClientesPage',
        component: SetorClientesPage,
      },
      {
        path: '/Relatorio/MultiploAcesso',
        name: 'MultiploAcessoRelatorio',
        component: MultiploAcessoRelatorio,
      },
    ],
  },
  {
    path: '/login',
    name: 'LoginPage',
    component: () => import('pages/LoginPage.vue'),
  },
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue'),
  },
];

export default routes;
