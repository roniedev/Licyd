<template>
  <jump-table-crud
    v-if="isTableVisible"
    :crud-table="tablePaginas"
    @new="toggleVisibility"
    @edit="edit"
    @show="show"
  />

  <q-card v-if="isFormVisible">
    <q-card-section>
      <q-tabs
        v-model="tab"
        dense
        class="text-grey"
        active-color="primary"
        indicator-color="primary"
        align="left"
      >
        <q-tab name="pagina" label="Página" />
        <q-tab name="acoes" label="Ações" v-if="isTabAcoesVisible" />
      </q-tabs>

      <q-tab-panels v-model="tab" animated>
        <q-tab-panel name="pagina" class="q-pa-none">
          <jump-form
            :form="formPagina"
            @submit="submitPagina"
            @cancel="toggleVisibility"
          />
        </q-tab-panel>
        <q-tab-panel name="acoes" class="q-pa-none" v-if="isTabAcoesVisible">
          <jump-form
            :form="formPaginaAcoes"
            @cancel="toggleVisibility"
            @submit="submitPaginaAcoes"
          >
            <template v-slot:right-actions-before>
              <q-btn
                flat
                size="md"
                color="primary"
                label="Inserir ações padrão"
                @click="inserirAcoesPadrao"
              />
            </template>
            <template v-slot:after-form>
              <jump-table :table="tablePaginaAcoes" />
            </template>
          </jump-form>
        </q-tab-panel>
      </q-tab-panels>
    </q-card-section>
  </q-card>
</template>

<script lang="ts">
import PaginaService from '../services/PaginaService';
import JumpForm from 'src/components/forms/JumpForm.vue';
import JumpTable from 'src/components/tables/JumpTable.vue';
import PaginaAcaoService from '../services/PaginaAcaoService';
import JumpTableCrud from 'src/components/tables/JumpTableCrud.vue';
import IJumpTableCrud from 'src/components/tables/interfaces/IJumpTableCrud';

import { defineComponent, ref } from 'vue';
import { FormElementType } from 'src/components/forms/enums/FormElementType';
import { IJumpForm } from 'src/components/forms/interfaces/IJumpForm';
import { IJumpInput } from 'src/components/forms/interfaces/IJumpInput';
import { IJumpSelectServer } from 'src/components/forms/interfaces/IJumpSelectServer';
import { IJumpToggle } from 'src/components/forms/interfaces/IJumpToggle';
import { ColumnType } from 'src/components/tables/enums/ColumnType';
import { IJumpTable } from 'src/components/tables/interfaces/IJumpTable';
import { Dialog } from 'quasar';

export default defineComponent({
  name: 'PaginaPage',

  components: {
    JumpForm,
    JumpTable,
    JumpTableCrud,
  },

  setup() {
    let isTableVisible = ref<boolean>(true);
    let isFormVisible = ref<boolean>(false);

    const tab = ref<string>('pagina');
    const service = new PaginaService('Pagina');
    const isTabAcoesVisible = ref<boolean>(false);
    const paginaAcaoService = new PaginaAcaoService('PaginaAcao');

    const formPagina = ref<IJumpForm>({
      fields: [
        {
          name: 'id',
          type: FormElementType.number,
          hidden: true,
        } as IJumpInput,
        {
          label: 'Nome',
          name: 'nome',
          style: 'outlined',
          type: FormElementType.text,
          cols: 4,
          dense: true,
        } as IJumpInput,
        {
          name: 'moduloId',
          label: 'Módulo',
          urlGetOptions: 'ConsultaPadrao/GetModulos/',
          style: 'outlined',
          type: FormElementType.selectServer,
          cols: 4,
          emitValue: true,
          dense: true,
          labelNameField: 'moduloNome',
        } as IJumpSelectServer,
        {
          label: 'Ícone',
          name: 'iconeMenu',
          style: 'outlined',
          type: FormElementType.text,
          cols: 4,
          dense: true,
        } as IJumpInput,
        {
          label: 'Componente',
          name: 'componente',
          style: 'outlined',
          type: FormElementType.text,
          cols: 4,
          dense: true,
        } as IJumpInput,
        {
          label: 'API Controller',
          name: 'apiController',
          style: 'outlined',
          type: FormElementType.text,
          cols: 4,
          dense: true,
        } as IJumpInput,
        {
          label: 'Mostrar no menu',
          name: 'isVisivelNoMenu',
          type: FormElementType.toggle,
          cols: 4,
          value: false,
          dense: true,
        } as IJumpToggle,
      ],
    });

    const formPaginaAcoes = ref<IJumpForm>({
      useToolipOnSubmitButton: true,
      tooltipTextSubmitButton: 'Adicionar ação',
      data: {},
      fields: [
        {
          name: 'id',
          type: FormElementType.number,
          hidden: true,
        } as IJumpInput,
        {
          name: 'paginaId',
          type: FormElementType.number,
          hidden: true,
        } as IJumpInput,
        {
          label: 'Código',
          name: 'codigo',
          style: 'outlined',
          type: FormElementType.text,
          cols: 6,
          dense: true,
        } as IJumpInput,
        {
          label: 'Nome',
          name: 'nome',
          style: 'outlined',
          type: FormElementType.text,
          cols: 6,
          dense: true,
        } as IJumpInput,
      ],
    });

    const tablePaginas = ref<IJumpTableCrud>({
      title: 'Listagem de páginas',
      service: service,
      componentName: 'PaginaPage',
      rows: [],
      isEditActiveRows: true,
      actions: [
        {
          name: 'Ações da página',
          async onClick(row) {
            await edit(row);
            tab.value = 'acoes';
          },
        },
      ],
      columns: [
        {
          label: '#',
          name: 'id',
          field: 'id',
          type: ColumnType.number,
        },
        {
          label: 'Nome',
          name: 'nome',
          field: 'nome',
          type: ColumnType.text,
        },
        {
          label: 'Módulo',
          name: 'moduloNome',
          field: 'moduloNome',
          type: ColumnType.number,
        },
        {
          label: 'Componente',
          name: 'componente',
          field: 'componente',
          type: ColumnType.text,
        },
        {
          label: 'API controller',
          name: 'apiController',
          field: 'apiController',
          type: ColumnType.text,
        },
        {
          label: 'Mostrar no menu',
          name: 'isVisivelNoMenu',
          field: 'isVisivelNoMenu',
          type: ColumnType.boolean,
        },
        {
          label: 'Ícone',
          name: 'icone',
          field: 'icone',
          type: ColumnType.text,
        },
        {
          label: 'Status',
          name: 'statusName',
          field: 'statusName',
          type: ColumnType.text,
          align: 'center',
        },
        {
          label: 'Motivo da inativação',
          name: 'reasonInactivation',
          field: 'reasonInactivation',
          type: ColumnType.text,
        },
        {
          label: 'Criado por',
          name: 'createdBy',
          field: 'createdBy',
          type: ColumnType.text,
        },
        {
          label: 'Criado em',
          name: 'createdAt',
          field: 'createdAt',
          type: ColumnType.datetime,
        },
        {
          label: 'Atualizado por',
          name: 'updatedBy',
          field: 'updatedBy',
          type: ColumnType.text,
        },
        {
          label: 'Atualizado em',
          name: 'updatedAt',
          field: 'updatedAt',
          type: ColumnType.datetime,
        },
      ],
    });

    const tablePaginaAcoes = ref<IJumpTable>({
      dense: true,
      rows: [],
      actions: [
        {
          name: 'Editar',
          icon: 'mdi-pencil-outline',
          async onClick(row) {
            await editAcao(row);
          },
        },
        {
          name: 'Excluir',
          icon: 'mdi-delete-outline',
          async onClick(row) {
            Dialog.create({
              title: 'Confirmar exclusão',
              message: 'Tem certeza que deseja excluir a ação?',
              position: 'top',
              persistent: true,
              cancel: true,
            }).onOk(async () => {
              await paginaAcaoService.delete(row.id);
              tablePaginaAcoes.value.rows = (
                tablePaginaAcoes.value.rows as Array<any>
              ).filter((item) => row.id != item.id);
            });
          },
        },
      ],
      columns: [
        {
          field: 'id',
          name: 'id',
          label: '#',
          type: ColumnType.number,
        },
        {
          field: 'codigo',
          name: 'codigo',
          label: 'Código',
          type: ColumnType.text,
        },
        {
          field: 'nome',
          name: 'nome',
          label: 'Nome',
          type: ColumnType.text,
        },
        {
          field: 'createdBy',
          name: 'createdBy',
          label: 'Criado por',
          type: ColumnType.text,
        },
        {
          field: 'createdAt',
          name: 'createdAt',
          label: 'Criado em',
          type: ColumnType.datetime,
        },
      ],
    });

    const toggleVisibility = () => {
      isTableVisible.value = !isTableVisible.value;
      isFormVisible.value = !isFormVisible.value;
      if (!isTabAcoesVisible.value) tab.value = 'pagina';
      if (isTableVisible.value) resetForm();
    };

    const submitPagina = async (formData: any) => {
      const data = formData.id
        ? await service.update(formData)
        : await service.insert(formData);

      if (data) {
        formPaginaAcoes.value.data = formPaginaAcoes.value.data
          ? formPaginaAcoes.value.data
          : {};

        formPaginaAcoes.value.data['paginaId'] = data.id;
        isTabAcoesVisible.value = true;
        if (formData.id) tab.value = 'acoes';
      }
    };

    const submitPaginaAcoes = async (formData: any) => {
      const data = formData.id
        ? await paginaAcaoService.update(formData)
        : await paginaAcaoService.insert(formData);

      if (data) {
        formPaginaAcoes.value.data = {};
        (tablePaginaAcoes.value.rows as Array<any>).push(data);
      }
    };

    const getById = async (id: number): Promise<any> => {
      const data = await service.getById(id);

      if (data) {
        formPagina.value.data = data;
        tablePaginaAcoes.value.rows = data.acoes;
        formPaginaAcoes.value.data['paginaId'] = data.id;
      }

      return data;
    };

    const edit = async (row: any) => {
      const data = await getById(row.id);

      if (data) {
        tab.value = 'pagina';
        isTabAcoesVisible.value = !isTabAcoesVisible.value;

        toggleVisibility();

        tablePaginaAcoes.value.actions?.forEach((action) => {
          action.visible = true;
        });
      }
    };

    const show = async (row: any) => {
      const data = await getById(row.id);

      if (data) {
        toggleVisibility();

        formPagina.value.disabled = true;
        formPaginaAcoes.value.disabled = true;
        formPagina.value.showSubmitButton = false;
        formPaginaAcoes.value.showSubmitButton = false;
        tablePaginaAcoes.value.actions?.forEach((action) => {
          action.visible = false;
        });

        isTabAcoesVisible.value = true;
      }
    };

    const inserirAcoesPadrao = () => {
      const acoes = [
        {
          paginaId: formPaginaAcoes.value.data['paginaId'],
          codigo: 'Activate',
          nome: 'Ativar registro',
        },
        {
          paginaId: formPaginaAcoes.value.data['paginaId'],
          codigo: 'Update',
          nome: 'Atualizar registro',
        },
        {
          paginaId: formPaginaAcoes.value.data['paginaId'],
          codigo: 'Delete',
          nome: 'Apagar registro',
        },
        {
          paginaId: formPaginaAcoes.value.data['paginaId'],
          codigo: 'Export',
          nome: 'Exportar registros',
        },
        {
          paginaId: formPaginaAcoes.value.data['paginaId'],
          codigo: 'Inactivate',
          nome: 'Inativar registro',
        },
        {
          paginaId: formPaginaAcoes.value.data['paginaId'],
          codigo: 'Insert',
          nome: 'Inserir registro',
        },
        {
          paginaId: formPaginaAcoes.value.data['paginaId'],
          codigo: 'GetAll',
          nome: 'Listar registros com filtro',
        },
        {
          paginaId: formPaginaAcoes.value.data['paginaId'],
          codigo: 'GetAllPaginated',
          nome: 'Listar registros com paginação e filtro',
        },
        {
          paginaId: formPaginaAcoes.value.data['paginaId'],
          codigo: 'GetById',
          nome: 'Listar registro pelo identificador',
        },
      ];

      acoes.forEach(async (element) => {
        const data = await paginaAcaoService.insert(element);
        if (data) (tablePaginaAcoes.value.rows as Array<any>).push(data);
      });
    };

    const getAcaoById = async (id: number): Promise<any> => {
      const data = await paginaAcaoService.getById(id);

      if (data) {
        formPaginaAcoes.value.data = data;
      }

      return data;
    };

    const editAcao = async (row: any) => {
      const data = await getAcaoById(row.id);
      if (data) formPaginaAcoes.value.data = data;
    };

    const resetForm = () => {
      formPagina.value.data = {};
      formPaginaAcoes.value.data = {};
      tablePaginaAcoes.value.rows = [];
      isTabAcoesVisible.value = false;
    };

    return {
      tab,
      tablePaginas,
      formPagina,
      isFormVisible,
      isTableVisible,
      formPaginaAcoes,
      tablePaginaAcoes,
      isTabAcoesVisible,
      edit,
      show,
      submitPagina,
      submitPaginaAcoes,
      toggleVisibility,
      inserirAcoesPadrao,
    };
  },
});
</script>
