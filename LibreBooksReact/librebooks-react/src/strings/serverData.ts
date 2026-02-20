export const serverData = {
    host: "https://localhost:5262",
    route(pathName: string) {
        return this.host + pathName;
    },
    verificationReasons: {
        registration: "REGISTRATION",
        passwordReset: "PASSWORD_RESET",
        emailChange: "EMAIL_CHANGE"
    }
}