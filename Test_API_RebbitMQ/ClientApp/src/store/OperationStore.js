import { runInAction } from 'mobx';
import { makeAutoObservable } from "mobx"
import { operationService } from '../services/OperationService';

export function OperationStore() {

    const store = {
        operations: [],
        totalCount: 0,
        size: 15,
        page: 1,
        getOperationsAsync: async () => {
            const response = await operationService.getPaginatedOperations(store.page, store.size);
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