export { SessionData } from './session-data-utils.ts'
export { TempData } from './tempdata.utils.ts'

function lowerFirstLetter(str: string): string {
    return str.charAt(0).toLowerCase() + str.slice(1);
}

function pageWasReloaded(): boolean {
    return (performance.getEntriesByType('navigation') as PerformanceNavigationTiming[]) // Explicitly cast to the specific type
        .some(entry => entry.type === 'reload');
}

export { lowerFirstLetter, pageWasReloaded }