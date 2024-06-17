<template>
  <div class="q-pa-md">
    <t-filtro
      :visible="filtroVisivel"
      :filter-props="tableProps.filtros"
      @update-filters="obterDadosComFiltros"
      @toggle-visibility="toggleFiltroVisibility"
    />
    <q-table
      class="q-pa-md tabela my-sticky-header-column-table"
      :class="congelarColunas"
      flat
      bordered
      ref="tableRef"
      :rows="linhas"
      :columns="colunas"
      row-key="id"
      :selection="tableProps.selecao"
      :separator="tableProps.bordas"
      v-model:selected="linhasSelecionadas"
      v-model:pagination="pagination"
      :loading="loading"
      @request="obterDados"
    >
      <template v-slot:top-left>
        <span class="text-h6">{{ tableProps.titulo }}</span>
      </template>

      <!-- <template v-slot:top-right="props"> -->
      <template v-slot:top-right>
        <slot name="acoes"></slot>
        <q-btn
          flat
          icon="fa-regular fa-file-excel"
          class="q-ml-sm q-pa-sm"
          title="Inserir registros via importação"
          label="Inserir (importação)"
          size="sm"
          @click="formImportacaoVisivel = !formImportacaoVisivel"
        />
        <q-btn
          flat
          icon="fa-regular fa-file-excel"
          @click="exportar"
          class="q-ml-sm q-pa-sm"
          title="Exportar dados para excel"
          label="exportar dados"
          size="sm"
        />
        <q-btn
          flat
          icon="fa-solid fa-plus"
          @click="inserir"
          title="Inserir um registro"
          class="q-ml-sm q-pa-sm"
          label="novo"
          size="sm"
        />
      </template>

      <template v-slot:header-selection="scope">
        <q-toggle size="xs" v-model="scope.selected" />
      </template>

      <template v-slot:body-selection="scope">
        <q-toggle size="xs" v-model="scope.selected" />
      </template>

      <template v-slot:body-cell="props">
        <q-td :props="props" style="font-size: 12px">
          <template v-if="props.col.name !== 'acoes'">
            <template v-if="typeof props.row[props.col.field] === 'boolean'">
              <q-toggle
                size="xs"
                v-model="props.row[props.col.field]"
                :true-value="true"
                :false-value="false"
                disable
              />
            </template>
            <template v-else>
              {{ formatarValor(props.value) }}
            </template>
          </template>
          <template v-else-if="acoes">
            <q-btn dense flat icon="more_horiz">
              <q-menu transition-show="jump-down" transition-hide="jump-up">
                <template v-for="(acao, index) in acoes" :key="index">
                  <q-item
                    @click="acao.onClick(props.row)"
                    clickable
                    v-close-popup
                  >
                    <q-item-section avatar>
                      <q-avatar
                        :icon="acao.icon || 'fa-solid fa-chevron-right'"
                        size="sm"
                      />
                    </q-item-section>
                    <q-item-section style="margin-right: 15px">
                      <q-item-label>{{ acao.nome }}</q-item-label>
                    </q-item-section>
                  </q-item>
                </template>
              </q-menu>
            </q-btn>
          </template>
        </q-td>
      </template>
    </q-table>
    <q-dialog v-model="formInativacaoVisivel">
      <t-form
        @submit="inativar"
        @cancelar="formInativacaoVisivel = !formInativacaoVisivel"
        :form="tableProps.formInativar || formInativacaoPadrao"
        style="width: 700px"
      />
    </q-dialog>
    <q-dialog v-model="formImportacaoVisivel">
      <t-form
        @submit="inserirViaImportacao"
        @cancelar="formImportacaoVisivel = !formImportacaoVisivel"
        :form="tableProps.formImportar || formImportacaoPadrao"
        style="width: 1000px"
      >
        <template #acoesEsquerda>
          <q-btn
            flat
            size="md"
            color="secondary"
            label="layout"
            icon="fa-regular fa-file-excel"
            @click="obterLayout"
          />
        </template>
      </t-form>
    </q-dialog>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, watch } from 'vue';
import { hideLoading, showLoading } from 'src/helpers/Loading';
import { useRouter } from 'vue-router';
import { IBaseCrudServiceOld } from 'src/interfaces/IBaseCrudServiceOld';
import {
  ITableAction,
  ITableColumn,
  ITableProps,
} from 'src/interfaces/components/ITableCrud';
import { IListarTodosRequest } from 'src/interfaces/IListarTodos';
import { Dialog } from 'quasar';
import { IForm } from 'src/interfaces/components/IForm';
import { IFiltro } from 'src/interfaces/components/IFiltro';

import moment from 'moment';
import util from 'src/utils/util';
import TForm from 'src/components/TForm/TForm.vue';
import TFiltro from './TFiltro.vue';

export default defineComponent({
  name: 'TTable',

  components: {
    TForm,
    TFiltro,
  },

  props: {
    service: {
      type: Object as () => IBaseCrudServiceOld,
      required: true,
    },
    tableProps: {
      type: Object as () => ITableProps,
      required: true,
    },
    formComponent: {
      type: String,
      required: true,
    },
    excelFilename: {
      type: String,
      required: false,
    },
    updateData: {
      type: Number,
      default: 0,
    },
    urlInativar: {
      type: String,
      default: '',
    },
  },

  methods: {
    formatarValor(valor: string) {
      if (util.isDate(valor)) return moment(valor).format('DD/MM/YYYY');
      else if (util.isDateTime(valor))
        return moment(valor).format('DD/MM/YYYY HH:mm:ss');

      return valor;
    },
  },

  computed: {
    congelarColunas() {
      let cssClass: string =
        this.tableProps?.selecao !== 'none'
          ? 'my-sticky-header-column-table'
          : '';

      return cssClass;
    },
  },

  emits: ['obterLinhasSelecionadas', 'atualizarDados'],

  setup(props) {
    const router = useRouter();
    const tableRef = ref();
    const formInativacaoVisivel = ref(false);
    const formImportacaoVisivel = ref(false);
    const filtroVisivel = ref(false);
    const loading = ref(false);
    const linhas = ref([]);
    const filtros = ref([] as IFiltro[]);
    const linhasSelecionadas = ref([]);
    const formInativacaoPadrao = ref({} as IForm);
    const formImportacaoPadrao = ref({} as IForm);

    const pagination = ref({
      sortBy: 'desc',
      descending: false,
      page: 1,
      rowsPerPage: 10,
      rowsNumber: 10,
    });

    let selectedRow: any;
    let acoes = ref<ITableAction[]>();
    let colunas = ref<ITableColumn[]>();

    watch(
      () => props.tableProps.colunas,
      (value: ITableColumn[]) => {
        colunas.value = value;
      },
      { immediate: true }
    );

    formInativacaoPadrao.value = {
      title: 'Inativar registro',
      fields: [
        {
          label: 'Motivo inativação',
          name: 'motivoInativacao',
          type: 'textarea',
          cols: 12,
          dense: true,
          design: 'outlined',
          required: true,
          requiredRules: [
            (val) => (val && val.length > 0) || 'O campo é obrigatório.',
            (val) =>
              (val && val.length <= 255) ||
              'O campo deve ter no máximo 255 caracteres.',
          ],
        },
      ],
    };

    formImportacaoPadrao.value = {
      title: 'Inserir via importação',
      fields: [
        {
          label: 'Arquivo',
          name: 'file',
          type: 'file',
          cols: 12,
          dense: true,
          design: 'outlined',
          required: true,
        },
      ],
    };

    watch(
      () => props.tableProps.acoes,
      (newValue: ITableAction[]) => {
        if (colunas.value) {
          var index = colunas.value.findIndex((item) => {
            return item.name === 'acoes';
          });

          if (index === -1) {
            colunas.value.unshift({
              name: 'acoes',
              required: true,
              align: 'center',
              label: 'Ações',
              field: 'acao',
              sortable: false,
              type: 'custom',
            });
          }

          acoes.value = [...newValue];

          if (
            props.tableProps.isEditarVisible !== false
              ? true
              : props.tableProps.isEditarVisible
          ) {
            acoes.value?.push({
              nome: 'Editar',
              icon: 'fa-regular fa-pen-to-square',
              onClick: editar,
            });
          }

          if (
            props.tableProps.isAtivarVisible !== false
              ? true
              : props.tableProps.isAtivarVisible
          ) {
            acoes.value?.push({
              nome: 'Ativar',
              icon: 'fa-solid fa-check',
              onClick: ativar,
            });
          }

          if (
            props.tableProps.isInativarVisible !== false
              ? true
              : props.tableProps.isInativarVisible
          ) {
            acoes.value?.push({
              nome: 'Copiar',
              icon: 'fa-solid fa-copy',
              onClick: clonar,
            });
          }

          if (
            props.tableProps.isInativarVisible !== false
              ? true
              : props.tableProps.isInativarVisible
          ) {
            acoes.value?.push({
              nome: 'Inativar',
              icon: 'fa-solid fa-xmark',
              onClick: mostrarFormInativacao,
            });
          }

          if (
            props.tableProps.isExcluirVisible !== false
              ? true
              : props.tableProps.isExcluirVisible
          ) {
            acoes.value?.push({
              nome: 'Excluir',
              icon: 'fa-regular fa-trash-can',
              onClick: excluir,
            });
          }

          // acoes.value = [
          //   ...newValue,
          //   { nome: 'Ativar', icon: 'fa-solid fa-check', onClick: ativar },
          //   { nome: 'Copiar', icon: 'fa-solid fa-copy', onClick: clonar },
          //   {
          //     nome: 'Excluir',
          //     icon: 'fa-regular fa-trash-can',
          //     onClick: excluir,
          //   },
          //   {
          //     nome: 'Inativar',
          //     icon: 'fa-solid fa-xmark',
          //     onClick: mostrarFormInativacao,
          //   },
          // ];
        }
      },
      { immediate: true }
    );

    if (acoes.value) {
      acoes.value.sort((a, b) => {
        const nomeA = a.nome.toUpperCase();
        const nomeB = b.nome.toUpperCase();

        if (nomeA < nomeB) {
          return -1;
        }

        if (nomeA > nomeB) {
          return 1;
        }

        return 0;
      });
    }

    async function ativar(row: any) {
      Dialog.create({
        title: 'Confirmação de ativação',
        message:
          'Tem certeza que deseja ativar o registro? Após a ativação ele não poderá mais ser editado.',
        cancel: true,
        persistent: true,
      }).onOk(async () => {
        showLoading();
        const sucesso = await props.service.ativar(row.id);
        if (sucesso) obterDados(pagination.value);
        hideLoading();
      });
    }

    async function clonar(linha: any) {
      Dialog.create({
        title: 'Confirmação de clone',
        message: 'Tem certeza que deseja clonar o registro?',
        cancel: true,
        persistent: true,
      }).onOk(async () => {
        showLoading();
        const objeto = await props.service.clonar(linha.id);
        if (objeto) obterDados(pagination.value);
        hideLoading();
      });
    }

    function mostrarFormInativacao(row: any) {
      selectedRow = row;
      formInativacaoVisivel.value = !formInativacaoVisivel.value;
    }

    async function inativar(formData: any) {
      const data = { ...formData };
      data.id = selectedRow.id;

      const sucesso = await props.service.inativar(data);

      if (sucesso) obterDados(pagination.value);

      formInativacaoVisivel.value = false;

      selectedRow = {};
    }

    function excluir(linha: any) {
      Dialog.create({
        title: 'Confirmação de exclusão',
        message: 'Tem certeza que deseja excluir o registro?',
        cancel: true,
        persistent: true,
      }).onOk(async () => {
        const sucesso = await props.service.excluir(linha.id);
        if (sucesso) obterDados(pagination.value);
      });
    }

    function inserir() {
      router.push({ name: props.formComponent });
    }

    function editar(row: any) {
      router.push({ name: props.formComponent, params: { id: row.id } });
    }

    async function obterDados(request: any) {
      loading.value = true;
      // Usar quando implementar o order by no server
      // const { page, rowsPerPage, sortBy, descending } = paginacao;

      const { page, rowsPerPage } = request.pagination || request;
      const skip = (page - 1) * rowsPerPage;
      const take = rowsPerPage;

      const data: IListarTodosRequest = {
        skip: skip,
        take: take,
        filtros: filtros.value,
      };

      const response = await props.service.obterTodos(data);

      if (response) {
        linhas.value = response.registros;
        pagination.value.page = page;
        pagination.value.rowsPerPage = rowsPerPage;
        pagination.value.rowsNumber = response.totalRegistros;

        // Usar quando implementar o order by no server
        // pagination.value.sortBy = sortBy;
        // pagination.value.descending = descending;
      }

      loading.value = false;
    }

    function obterDadosComFiltros(props: IFiltro[]) {
      pagination.value.page = 1;
      filtros.value = props;
      obterDados(pagination.value);
    }

    async function exportar() {
      await props.service.exportar(
        filtros.value,
        props.excelFilename || 'exportacao'
      );
    }

    async function obterLayout() {
      await props.service.obterInserirLayout();
    }

    function toggleFiltroVisibility() {
      filtroVisivel.value = !filtroVisivel.value;
    }

    async function inserirViaImportacao(formData: any) {
      if (props.tableProps?.formImportar?.onSubmit) {
        props.tableProps?.formImportar?.onSubmit(formData);
      } else {
        await props.service.inserirViaImportacao(formData);
      }

      formImportacaoVisivel.value = false;
      obterDados(pagination.value);
    }

    watch(
      () => props.updateData,
      () => obterDados(pagination.value)
    );

    onMounted(() => {
      tableRef.value.requestServerInteraction();
    });

    return {
      tableRef,
      colunas,
      acoes,
      formInativacaoVisivel,
      formImportacaoVisivel,
      pagination,
      filtroVisivel,
      loading,
      linhas,
      filtros,
      linhasSelecionadas,
      formInativacaoPadrao,
      formImportacaoPadrao,
      mostrarFormInativacao,
      inativar,
      obterDados,
      obterDadosComFiltros,
      toggleFiltroVisibility,
      inserir,
      exportar,
      obterLayout,
      inserirViaImportacao,
    };
  },
});
</script>

<style lang="sass">
.my-sticky-header-column-table
  max-width: 100%

  // td:first-child
  //   background-color: #FAFAFA

  tr th:first-child
    position: sticky
    z-index: 2
    // background: #FAFAFA

  thead tr:last-child th
    top: 48px
    z-index: 3

  thead tr:first-child th
    top: 0
    z-index: 1

  tr:first-child th:first-child
    z-index: 3

  td:first-child
    z-index: 1

  td:first-child, th:first-child
    position: sticky
    left: 0

  tbody
    scroll-margin-top: 48px

.my-sticky-last-column-table
  max-width: 100%

  // thead tr:last-child th:last-child
  //   background: #FAFAFA

  // td:last-child
  //   background: #FAFAFA

  th:last-child,
  td:last-child
    position: sticky
    right: 0
    z-index: 1

.tabela
  border: 1px solid rgb(0, 0, 0, 0.20)
</style>
