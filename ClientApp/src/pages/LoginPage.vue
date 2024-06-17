<template>
  <div class="row" style="height: 100vh">
    <div
      class="col-0 col-md-3 flex justify-center content-center"
      style="background-color: #fff"
    >
      <img src="img/logo-teuto2.png" class="responsive" alt="Logo Teuto" />
    </div>
    <div
      class="col-12 col-md-9"
      style="background-color: #004170"
      :class="
        $q.screen.sm || $q.screen.xs
          ? 'top-login'
          : 'flex justify-center content-center'
      "
    >
      <div class="row">
        <div
          class="col-12 flex justify-center"
          v-if="$q.screen.sm || $q.screen.xs"
        >
          <img
            src="img/logo-branca.png"
            :style="
              $q.screen.lt.sm || $q.screen.xs
                ? { width: '80%' }
                : { width: '30%' }
            "
            class="responsive"
            alt="Logo Teuto"
          />
        </div>
        <div class="col-12 flex justify-center">
          <q-card
            flat
            class="q-pb-md q-ma-lg q-pa-sm"
            :style="
              $q.screen.lt.sm || $q.screen.xs
                ? { width: '100%' }
                : { width: '400px' }
            "
          >
            <q-card-section>
              <div>
                <div class="col text-h5 ellipsis flex justify-center">
                  <h2
                    class="text-h5 text-uppercase q-my-none text-weight-regular"
                  >
                    {{ aplicacaoCodigo }}
                  </h2>
                </div>
                <div class="col ellipsis flex justify-center q-mt-xs">
                  <p class="text-subtitle2 q-my-none text-weight-regular">
                    {{ aplicacaoNome }}
                  </p>
                </div>
              </div>
              <q-form class="q-gutter-md q-mt-md" @submit.prevent="onSubmit">
                <q-input
                  label="Usuário de rede"
                  v-model="usuarioRede"
                  hide-bottom-space
                  :rules="[(val) => !!val || 'Informe o usuário de rede']"
                >
                  <template v-slot:prepend>
                    <q-icon name="mdi-account-outline" size="xs" />
                  </template>
                </q-input>
                <q-input
                  label="Senha"
                  type="password"
                  v-model="senha"
                  hide-bottom-space
                  :rules="[(val) => !!val || 'Informe a senha']"
                >
                  <template v-slot:prepend>
                    <q-icon name="mdi-lock-outline" size="xs" />
                  </template>
                </q-input>
                <div>
                  <q-btn
                    class="full-width"
                    color="primary"
                    label="entrar"
                    type="submit"
                    dense
                  ></q-btn>
                </div>
              </q-form>
            </q-card-section>
          </q-card>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import ILogin from 'src/interfaces/ILogin';
import AuthService from 'src/services/AuthService';
import RouterService from 'src/services/RouterService';
import useAutenticacaoStore from 'src/stores/autenticacao.store';
import { defineComponent, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';

export default defineComponent({
  name: 'LoginPage',

  setup() {
    const route = useRoute();
    const router = useRouter();
    const service = AuthService;
    const useAutenticacao = useAutenticacaoStore();
    const routerService = new RouterService('menu', router);

    const senha = ref('');
    const usuarioRede = ref('');
    const aplicacaoNome = ref(process.env.APLICACAO_NOME);
    const aplicacaoCodigo = ref(process.env.APLICACAO_CODIGO);
    const identificador = ref(process.env.IDENTIFICADOR);

    async function onSubmit() {
      const loginValues: ILogin = {
        usuarioRede: usuarioRede.value,
        senha: senha.value,
        identificador: identificador.value || '',
      };

      var isLogged = await service.login(loginValues);

      if (isLogged && useAutenticacao.authenticated && useAutenticacao.user) {
        const routes = useAutenticacao.routes;
        routerService.removerRotasSemPermisao(routes);

        const redirect = route.query.redirect as string | undefined;
        router.push(redirect || { name: 'IndexPage' });
      }
    }

    return {
      usuarioRede,
      senha,
      aplicacaoNome,
      aplicacaoCodigo,
      onSubmit,
    };
  },
});
</script>

<style lang="sass" scoped>
.img-principal-login
  background-image: url('/img/logo-app.png')
  background-size: cover
  background-position: center

.sessao-direita
  height: 100vh
  background-color: $primary

.sessao-esquerda
  background-color: #FFF !important

.app-nome-login
  font-weight: bold
  font-size: 35px
  margin: 0
  padding: 0

.q-card

  .text-h5
    font-size: 22.5px

  .q-icon
    font-size: 25px

  .q-btn
    margin-top: 10px

.q-input
  margin-bottom: 10px

.top-login
  padding-top: 125px
</style>
