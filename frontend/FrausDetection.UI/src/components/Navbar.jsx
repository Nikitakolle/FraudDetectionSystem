import { Link, useNavigate }
    from 'react-router-dom'

import '../App.css'

function Navbar() {

    const navigate = useNavigate()

    // ================= LOGOUT =================

    const handleLogout = () => {

        localStorage.removeItem('token')

        navigate('/login')
    }

    return (

        <div className="navbar">

            {/* ================= LEFT ================= */}

            <div className="navbar-logo">

                Fraud Detection Dashboard

            </div>

            {/* ================= RIGHT ================= */}

            <div className="navbar-links">

                <Link
                    to="/"
                    className="navbar-link"
                >

                    Home

                </Link>

                <Link
                    to="/history"
                    className="navbar-link"
                >

                    History

                </Link>

                <button
                    className="logout-button"
                    onClick={handleLogout}
                >

                    Logout

                </button>

            </div>

        </div>
    )
}

export default Navbar