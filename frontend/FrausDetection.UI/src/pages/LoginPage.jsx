import { useState } from 'react'

import axios from 'axios'

import {
    useNavigate,
    Link
} from 'react-router-dom'

import '../App.css'

function LoginPage() {

    // ================= STATES =================

    const [username, setUsername]
        = useState('')

    const [password, setPassword]
        = useState('')

    const [errorMessage, setErrorMessage]
        = useState('')

    const navigate = useNavigate()

    // ================= LOGIN =================

    const handleLogin = async () => {

        try {

            setErrorMessage('')

            const response = await axios.post(

                'https://localhost:7206/api/Auth/login',

                {
                    username,
                    password
                }
            )

            // ================= SAVE TOKEN =================

            localStorage.setItem(
                'token',
                response.data.token
            )

            // ================= REDIRECT =================

            navigate('/')

        }
        catch (error) {

            console.error(error)

            setErrorMessage(
                'Invalid username or password'
            )
        }
    }

    // ================= UI =================

    return (

        

        <div className="login-page">

            <div className="login-card">

                <h1 className="login-title">

                    Fraud Detection Dashboard

                </h1>

                <p className="login-subtitle">

                    Real-Time Transaction Risk Assessment and Fraud Prevention

                </p>

                {/* ================= USERNAME ================= */}

                <div className="input-group">

                    <label>Username</label>

                    <input
                        type="text"
                        placeholder="Enter username"
                        value={username}
                        onChange={(event) =>
                            setUsername(
                                event.target.value
                            )
                        }
                    />

                </div>

                {/* ================= PASSWORD ================= */}

                <div className="input-group">

                    <label>Password</label>

                    
                    <input
                        type="password"
                        placeholder="Enter password"
                        value={password}
                        onChange={(event) =>
                            setPassword(
                                event.target.value
                            )
                        }

                        onKeyDown={(event) => {

                            if (event.key === 'Enter') {

                                handleLogin()
                            }
                        }}
                    />
                </div>

                {/* ================= ERROR ================= */}

                {
                    errorMessage && (

                        <div className="error-message">

                            {errorMessage}

                        </div>
                    )
                }

                {/* ================= BUTTON ================= */}

                <button
                    className="login-button"
                    onClick={handleLogin}
                >

                    Login

                </button>
                <p
                    style={{
                        textAlign: 'center',
                        marginTop: '20px'
                    }}
                >

                    Don't have an account?

                    <Link
                        to="/register"
                        style={{
                            marginLeft: '5px'
                        }}
                    >

                        Register

                    </Link>

                </p>

            </div>

        </div>
    
    )
}

export default LoginPage