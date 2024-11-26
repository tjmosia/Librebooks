
export default function useHttp() {
    const mimeTypes = {
        Json: 'application/json',
        JsonLd: "application/ld+json",
        Js: "text/javascript",
        JSModule: "text/javascript",
        Png: "image/png",
        PDF: "application/pdf",
        Txt: "text/plain"
    }
    return {
        headers: {
            contentType: {
                name: "Content-Type",
                values: mimeTypes
            },
            Auth: {
                name: "Authorization"
            }
        },
        mimeTypes,
        Auth: {

        }
    }
}