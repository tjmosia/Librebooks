export interface IUser {
    readonly email: string;
    readonly firstName: string;
    readonly lastName: string;
    readonly photo?: string;
}

export interface IClaim {
    type: string;
    value: string
}

export interface IRole {
    name: string;
    associatedTo: string
}

export interface IClaimsPrincipal {
    user: IUser
    claims: IClaim[],
    roles: IRole[]
}

export interface IIdentity {
    claimsPrincipal?: IClaimsPrincipal
}