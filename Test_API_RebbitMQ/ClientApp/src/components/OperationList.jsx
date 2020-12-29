import React, { useEffect, useState } from 'react';
import { operationService } from '../services/OperationService';
import { DataGrid } from '@material-ui/data-grid';

const columns = [
  { field: 'id', headerName: 'Id', width: 50, sortable: false },
  { field: 'name', headerName: 'Operation Name', width: 200, sortable: false },
  { field: 'userName', headerName: 'User Name', width: 200, sortable: false },
  { field: 'creationDate', headerName: 'Creation Date', width: 200, sortable: false },
  { field: 'status', headerName: 'Status', width: 200, sortable: false },
  { field: 'executionTime', headerName: 'Execution Time', width: 200, sortable: false },
];


export function OperationList() {
  const[tableState, setTableState] = useState({operations: [], totalCount: 0, page: 1});
  const size = 15;

  useEffect(() => {
    operationService.getPaginatedOperations(tableState.page, size).then(res => {
      setTableState({...res.data, page: 1});
    });
  }, []);

  const onPageChange = (p) => {

    if (p.page !== tableState.page) {
      //setPage(p.page);
      operationService.getPaginatedOperations(p.page, size).then(res => {
        setTableState({...res.data, page: p.page});
      });
    }
  }

  return (
    <div style={{ height: 800, width: '100%', minWidth: 1000 }}>
      <DataGrid rows={tableState.operations}
        autoHeight
        columns={columns}
        paginationMode='server'
        rowCount={tableState.totalCount}
        page={tableState.page}
        onPageChange={onPageChange}
        pageSize={size}
      />
    </div>
  );

}
