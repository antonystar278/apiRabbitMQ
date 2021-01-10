import { runInAction } from 'mobx';
import { makeAutoObservable } from "mobx"
import { OperationService } from '../services/OperationService';

export function OperationStore() {
    const operationService = new OperationService();

    const store = {
        operations: [],
        totalCount: 0,
        size: 15,
        page: 1,
        getOperationsAsync: async () => {
            const response = await operationService.getFilteredOperations(store.page, store.size);
            runInAction(() => {
                store.operations = response.data.operations;
                store.totalCount = response.data.totalCount;
            });

        },
        createOperationAsync: async (name, userId) => {
            const response = await operationService.createOperation(name, userId);
            await store.getOperationsAsync(store.page, store.size);
        },
        setPage: (page) => { store.page = page }
    }
    return makeAutoObservable(store);
}