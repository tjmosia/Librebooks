export interface ISendVerificationCodeResponse {
    codeHashString: string
}

export interface IVerifyCodeResponse extends ISendVerificationCodeResponse {
    code: string
}

export interface IAuthVerification {
    code?: string
    codeHashString?: string
}

export interface ISendVerificationCodeApiModel {
    email: string
    reason?: string
}
