<template>
  <jump-table-crud
    :crudTable="grupoTableCrud"
    v-if="isTableVisible"
    @new="toggleVisibility"
    @edit="edit"
  />

  <q-card v-if="!isTableVisible">
    <q-card-section>
      <q-tabs v-model="tab" align="left">
        <q-tab name="grupos" label="Grupo" />
        <q-tab name="paginas" label="Página" v-if="grupoId" />
        <q-tab name="usuarios" label="Usuário" v-if="grupoId" />
      </q-tabs>

      <q-separator />

      <div class="q-gutter-y-sm">
        <q-tab-panels
          v-model="tab"
          animated
          transition-prev="slide-right"
          transition-next="slide-left"
        >
          <q-tab-panel name="grupos">
            <jump-form
              :form="grupoForm"
              @submit="handleSubmit"
              @cancel="toggleVisibility"
            />
          </q-tab-panel>

          <q-tab-panel name="paginas">
            <jump-table-crud
              :crudTable="grupoPaginaTableCrud"
              v-if="isGrupoPaginaTableVisible"
              @new="grupoPaginaToggleVisibility"
              @edit="grupoPaginaEdit"
            />

            <jump-form
              :form="grupoPaginaForm"
              @submit="grupoPaginaHandleSubmit"
              @cancel="grupoPaginaToggleVisibility"
              v-if="!isGrupoPaginaTableVisible"
            />
          </q-tab-panel>

          <q-tab-panel name="usuarios">
            <jump-table-crud
              :crudTable="grupoUsuarioTableCrud"
              v-if="isGrupoUsuarioTableVisible"
              @new="grupoUsuarioToggleVisibility"
            />

            <jump-form
              :form="grupoUsuarioForm"
              @submit="grupoUsuarioHandleSubmit"
              @cancel="grupoUsuarioToggleVisibility"
              v-if="!isGrupoUsuarioTableVisible"
            />
          </q-tab-panel>
        </q-tab-panels>
      </div>
    </q-card-section>
  </q-card>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { ColumnType } from 'src/components/tables/enums/ColumnType';
import { IJumpForm } from 'src/components/forms/interfaces/IJumpForm';
import { IJumpInput } from 'src/components/forms/interfaces/IJumpInput';
import { FormElementType } from 'src/components/forms/enums/FormElementType';
import { IJumpSelectServer } from 'src/components/forms/interfaces/IJumpSelectServer';

import GrupoService from '../services/GrupoService';
import JumpForm from 'src/components/forms/JumpForm.vue';
import GrupoPaginaService from '../services/GrupoPaginaService';
import GrupoUsuarioService from '../services/GrupoUsuarioService';
import JumpTableCrud from 'src/components/tables/JumpTableCrud.vue';
import IJumpTableCrud from 'src/components/tables/interfaces/IJumpTableCrud';

export default defineComponent({
  name: 'GrupoPage',

  components: {
    JumpTableCrud,
    JumpForm,
  },

  setup() {
    let grupoId = ref<number>();

    const tab = ref<string>('grupos');
    const isTableVisible = ref<boolean>(true);
    const isGrupoPaginaTableVisible = ref<boolean>(true);
    const isGrupoUsuarioTableVisible = ref<boolean>(true);

    const service = new GrupoService('grupo');
    const grupoPaginaService = new GrupoPaginaService('GrupoPagina');
    const grupoUsuarioService = new GrupoUsuarioService('GrupoUsuario');

    const grupoTableCrud = ref<IJumpTableCrud>({
      title: 'Listagem de grupos',
      service: service,
      componentName: 'GrupoPage',
      rows: [],
      isEditActiveRows: true,
      columns: [
        {
          label: '#',
          field: 'id',
          name: 'id',
          type: ColumnType.number,
        },
        {
          label: 'Nome',
          field: 'nome',
          name: 'nome',
          type: ColumnType.text,
        },
        {
          label: 'Observação',
          field: 'observacao',
          name: 'observacao',
          type: ColumnType.text,
        },
        {
          label: 'Status',
          field: 'statusName',
          name: 'statusName',
          type: ColumnType.text,
        },
        {
          label: 'Motivo da inativação',
          field: 'reasonInactivation',
          name: 'reasonInactivation',
          type: ColumnType.text,
        },
        {
          label: 'Criado por',
          field: 'createdBy',
          name: 'createdBy',
          type: ColumnType.text,
        },
        {
          label: 'Criado em',
          field: 'createdAt',
          name: 'createdAt',
          type: ColumnType.datetime,
        },
        {
          label: 'Atualizado por',
          field: 'updatedBy',
          name: 'updatedBy',
          type: ColumnType.text,
        },
        {
          label: 'Atualizado em',
          field: 'updatedAt',
          name: 'updatedAt',
          type: ColumnType.datetime,
        },
      ],
    });

    const grupoForm = ref<IJumpForm>({
      fields: [
        {
          name: 'id',
          hidden: true,
          type: FormElementType.number,
        } as IJumpInput,
        {
          label: 'Nome',
          name: 'nome',
          cols: 6,
          type: FormElementType.text,
          style: 'outlined',
        } as IJumpInput,
        {
          label: 'Observação',
          name: 'observacao',
          cols: 6,
          type: FormElementType.text,
          style: 'outlined',
        } as IJumpInput,
      ],
    });

    const grupoPaginaTableCrud = ref<IJumpTableCrud>({
      title: 'Páginas do grupo',
      service: grupoPaginaService,
      componentName: 'GrupoPage',
      isEditActiveRows: true,
      rows: [],
      filters: [
        {
          name: 'GrupoId',
          value: grupoId.value ? grupoId.value.toString() : '0',
        },
      ],
      columns: [
        {
          label: '#',
          field: 'id',
          name: 'id',
          type: ColumnType.number,
        },
        {
          label: 'Página',
          field: 'paginaNome',
          name: 'paginaNome',
          type: ColumnType.text,
        },
        {
          label: 'Criado por',
          field: 'createdBy',
          name: 'createdBy',
          type: ColumnType.text,
        },
        {
          label: 'Criado em',
          field: 'createdAt',
          name: 'createdAt',
          type: ColumnType.datetime,
        },
        {
          label: 'Atualizado por',
          field: 'updatedBy',
          name: 'updatedBy',
          type: ColumnType.text,
        },
        {
          label: 'Atualizado em',
          field: 'updatedAt',
          name: 'updatedAt',
          type: ColumnType.datetime,
        },
      ],
    });

    const grupoPaginaForm = ref<IJumpForm>({
      data: {},
      fields: [
        {
          name: 'id',
          hidden: true,
          type: FormElementType.number,
        } as IJumpInput,
        {
          name: 'grupoId',
          hidden: true,
          type: FormElementType.number,
        } as IJumpInput,
        {
          label: 'Página',
          name: 'paginaId',
          cols: 12,
          type: FormElementType.selectServer,
          style: 'outlined',
          urlGetOptions: 'ConsultaPadrao/GetPaginas',
          labelNameField: 'paginaNome',
          urlParams: [
            {
              fieldName: 'grupoId',
              paramName: 'grupoId',
            },
          ],
        } as IJumpSelectServer,
        {
          label: 'Ações',
        name: 'acoes',
          cols: 12,
          type: FormElementType.selectServer,
          urlGetOptions: 'ConsultaPadrao/GetPaginaAcoes',
          multiselect: true,
          behavior: 'dialog',
          useChips: true,
          style: 'outlined',
          optionLabel: 'acaoNome',
          optionValue: 'PaginaAcaoId',
          emitValue: false,
          urlParams: [
            {
              fieldName: 'paginaId',
              paramName: 'paginaId',
            },
          ],
        } as IJumpSelectServer,
      ],
    });

    const grupoUsuarioTableCrud = ref<IJumpTableCrud>({
      title: 'Usuários do grupo',
      service: grupoUsuarioService,
      componentName: 'GrupoPage',
      rows: [],
      filters: [
        {
          name: 'GrupoId',
          value: grupoId.value ? grupoId.value.toString() : '0',
        },
      ],
      columns: [
        {
          label: '#',
          field: 'id',
          name: 'id',
          type: ColumnType.number,
        },
        {
          label: 'Usuário',
          field: 'usuarioNome',
          name: 'usuarioNome',
          type: ColumnType.text,
        },
        {
          label: 'Criado por',
          field: 'createdBy',
          name: 'createdBy',
          type: ColumnType.text,
        },
        {
          label: 'Criado em',
          field: 'createdAt',
          name: 'createdAt',
          type: ColumnType.datetime,
        },
        {
          label: 'Atualizado por',
          field: 'updatedBy',
          name: 'updatedBy',
          type: ColumnType.text,
        },
        {
          label: 'Atualizado em',
          field: 'updatedAt',
          name: 'updatedAt',
          type: ColumnType.datetime,
        },
      ],
    });

    const grupoUsuarioForm = ref<IJumpForm>({
      data: {},
      fields: [
        {
          name: 'id',
          hidden: true,
          type: FormElementType.number,
        } as IJumpInput,
        {
          label: 'Usuário',
          name: 'usuarioId',
          cols: 12,
          type: FormElementType.selectServer,
          style: 'outlined',
          urlGetOptions: 'ConsultaPadrao/GetUsuarios',
        } as IJumpSelectServer,
      ],
    });

    const toggleVisibility = () => {
      isTableVisible.value = !isTableVisible.value;
    };

    const grupoPaginaToggleVisibility = () => {
      isGrupoPaginaTableVisible.value = !isGrupoPaginaTableVisible.value;
    };

    const grupoUsuarioToggleVisibility = () => {
      isGrupoUsuarioTableVisible.value = !isGrupoUsuarioTableVisible.value;
    };

    const handleSubmit = async (formData: any) => {
      const data = formData.id
        ? await service.update(formData)
        : await service.insert(formData);

      if (data) {
        grupoId.value = data.id;
        grupoPaginaForm.value.data['grupoId'] = data.id;
      }
    };

    const grupoPaginaHandleSubmit = async (formData: any) => {
      formData.grupoId = grupoId.value;

      const data = formData.id
        ? await grupoPaginaService.update(formData)
        : await grupoPaginaService.insert(formData);

      if (data) {
        if (grupoPaginaTableCrud.value.rows)
          grupoPaginaTableCrud.value.rows = [];

        grupoPaginaTableCrud.value.rows.push(data);
        grupoPaginaToggleVisibility();
      }
    };

    const grupoUsuarioHandleSubmit = async (formData: any) => {
      formData.grupoId = grupoId.value;

      const data = formData.id
        ? await grupoUsuarioService.update(formData)
        : await grupoUsuarioService.insert(formData);

      if (data) {
        if (grupoUsuarioTableCrud.value.rows)
          grupoUsuarioTableCrud.value.rows = [];

        grupoUsuarioTableCrud.value.rows.push(data);
        grupoUsuarioToggleVisibility();
      }
    };

    const getById = async (id: number) => {
      const data = await service.getById(id);

      if (data) {
        grupoId.value = data.id;
        grupoForm.value.data = data;
        grupoPaginaTableCrud.value.rows = data.paginas;
        grupoUsuarioTableCrud.value.rows = data.usuarios;
        grupoPaginaForm.value.fields.forEach((field) => {
          if (field.name === 'paginaId') {
            if ((field as IJumpSelectServer).urlParams) {
              (field as IJumpSelectServer).urlParams[0].value =
                grupoId.value?.toString();
            }
          }
        });
      }
    };

    const edit = async (row: any) => {
      await getById(row.id);
      toggleVisibility();
    };

    const grupoPaginaGetById = async (id: number) => {
      const data = await grupoPaginaService.getById(id);
      if (data) grupoPaginaForm.value.data = data;
    };

    const grupoPaginaEdit = async (row: any) => {
      await grupoPaginaGetById(row.id);
      grupoPaginaToggleVisibility();
    };

    return {
      tab,
      grupoId,
      grupoForm,
      isTableVisible,
      grupoTableCrud,
      grupoPaginaForm,
      grupoPaginaTableCrud,
      isGrupoPaginaTableVisible,
      grupoUsuarioTableCrud,
      grupoUsuarioForm,
      isGrupoUsuarioTableVisible,
      handleSubmit,
      toggleVisibility,
      grupoPaginaToggleVisibility,
      grupoUsuarioToggleVisibility,
      grupoPaginaHandleSubmit,
      grupoUsuarioHandleSubmit,
      edit,
      grupoPaginaEdit,
    };
  },
});
</script>
