export interface ITransactionError {
    code?: string | null
    description?: string | null
}

export interface ITransactionResult<T> {
    succeeded: boolean
    model?: T | null
    errors: ITransactionError[]
}