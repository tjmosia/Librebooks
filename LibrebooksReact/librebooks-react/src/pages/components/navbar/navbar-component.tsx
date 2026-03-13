import './navbar-component.scss'

interface NavbarComponentProps {
    children: React.ReactElement | null
}

export function NavbarComponent({ children }: NavbarComponentProps) {
    return (<nav className="navbar">
        <div className='navbar-content'>
            {children}
        </div>
    </nav>)
}