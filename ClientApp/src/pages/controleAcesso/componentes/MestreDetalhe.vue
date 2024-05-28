<template>
  <cadastro-mestre-detalhe :cadastro="cadastro" />
</template>

<script lang="ts">
import { ref, defineComponent } from 'vue';
import { ColumnType } from 'src/components/tables/enums/ColumnType';
import { IJumpInput } from 'src/components/forms/interfaces/IJumpInput';
import { IJumpToggle } from 'src/components/forms/interfaces/IJumpToggle';
import { FormElementType } from 'src/components/forms/enums/FormElementType';
import { IJumpSelectServer } from 'src/components/forms/interfaces/IJumpSelectServer';

import PaginaService from '../pagina/services/PaginaService';
import ICadastroMestreDetalhe from 'src/components/crud/interfaces/ICadastroMestreDetalhe';
import CadastroMestreDetalhe from 'src/components/crud/CadastroMestreDetalhe.vue';
import PaginaAcaoService from '../pagina/services/PaginaAcaoService';

export default defineComponent({
  name: 'MestreDetalhe',

  components: {
    CadastroMestreDetalhe,
  },

  setup() {
    const cadastro = ref<ICadastroMestreDetalhe>({
      parentKey: 'paginaId',
      tableMaster: {
        title: 'Listagem de páginas',
        service: new PaginaService('Pagina'),
        componentName: 'PaginaPage',
        rows: [],
        isEditActiveRows: true,
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
      },
      formMaster: {
        title: 'Cadastro de páginas',
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
      },
      tableDetail: {
        componentName: 'PaginaPage',
        rows: [],
        service: new PaginaAcaoService('paginaAcao'),
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
      },
      formDetail: {
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
      },
    });

    return {
      cadastro,
    };
  },
});
</script>
