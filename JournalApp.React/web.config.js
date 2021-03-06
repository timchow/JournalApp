export const Config = {
    SERVER_URL: "http://localhost:61121",
    SERVER_API_BASE: "api",
    GOOGLE: {
        API_CLIENT_ID: "425042187496-r380dek5d1333hmvp4p4tfkf5podeaja.apps.googleusercontent.com",
        SCOPES: "https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me",
        REDIRECT_PATH: "/GoogleAuth",
        get REDIRECT_URI() {
            return `${window.location.origin}${this.REDIRECT_PATH}`
        }
    },
    FACEBOOK: {
        API_CLIENT_ID: "1702234556491941",
        SCOPES: "email",
        REDIRECT_PATH: "/FacebookAuth",
        get REDIRECT_URI() {
            return `${window.location.origin}${this.REDIRECT_PATH}`
        }
    },MOCK: { ON: true, TOKEN: "mock" }
}