import { useRoute, useRouter } from 'vue-router';

export function routerHelper() {
  const router = useRouter();
  const route = useRoute();

  function obterIdParametro(): number | null {
    const id = route.params?.id;

    if (id === undefined || id === '') {
      return null;
    }

    return parseInt(typeof id === 'object' ? id[0] : id);
  }

  function levarUsuarioPara(url: string) {
    router.push(url);
  }

  return { obterIdParametro, levarUsuarioPara, router, route };
}
