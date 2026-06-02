import { Navigate } from 'react-router-dom'

function ProtectedRoute({ children }) {

    // ================= GET TOKEN =================

    const token =
        localStorage.getItem('token')

    // ================= CHECK TOKEN =================

    if (!token) {

        return <Navigate to="/login" />
    }

    // ================= ALLOW ACCESS =================

    return children
}

export default ProtectedRoute