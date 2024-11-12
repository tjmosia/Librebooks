

export function usePageTitle(title: string) {
	document.getElementsByTagName("title")[0].innerHTML = title + " | Oskit"
}