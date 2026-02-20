export { SessionData } from './session-data-utils.ts'
export { TempData } from './tempdata.utils.ts'

function lowerFirstLetter(str: string): string {
    return str.charAt(0).toLowerCase() + str.slice(1);
}

export { lowerFirstLetter }