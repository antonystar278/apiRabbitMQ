import React, { useContext, useEffect, useState } from 'react';
import { operationService } from '../services/OperationService';
import { DataGrid } from '@material-ui/data-grid';
import { observer } from 'mobx-react';
import { StoreContext } from '../store/StoreContext';


const columns = [
  { field: 'id', headerName: 'Id', width: 50, sortable: false },
  { field: 'name', headerName: 'Operation Name', width: 200, sortable: false },
  { field: 'userName', headerName: 'User Name', width: 200, sortable: false },
  { field: 'creationDate', headerName: 'Creation Date', width: 200, sortable: false },
  { field: 'status', headerName: 'Status', width: 200, sortable: false },
  { field: 'executionTime', headerName: 'Execution Time', width: 200, sortable: false },
];


export const OperationList = observer(() => {
  const store = useContext(StoreContext);

  useEffect(() => {
    store.getOperationsAsync();
  }, []);

  const onPageChange = (p) => {

    if (p.page !== store.page) {
      store.setPage(p.page);
      store.getOperationsAsync();
    }
  }

  return (
    <div style={{ height: 800, width: '100%', minWidth: 1000 }}>
      <DataGrid rows={store.operations}
        autoHeight
        columns={columns}
        paginationMode='server'
        rowCount={store.totalCount}
        page={store.page}
        onPageChange={onPageChange}
        pageSize={store.size}
      />
    </div>
  );

})
