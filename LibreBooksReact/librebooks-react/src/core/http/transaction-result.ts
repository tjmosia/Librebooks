import {type ITransactionError} from "./transaction-error"

export interface ITransactionResult<T = undefined> {
    succeeded: boolean
    errors: ITransactionError[]
    model?: T
}