

export interface ITransactionError {
    code: string
    description: string
}

export interface ITransactionResult<T> {
    succeeded: boolean
    model: T
    errors: ITransactionError[]
}