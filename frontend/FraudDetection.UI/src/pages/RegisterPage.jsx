import { useState } from 'react'

import api from '../axiosConfig'

import { toast } from 'react-toastify'
import { useNavigate, Link } from 'react-router-dom'

import '../App.css'

function RegisterPage() {

    const [username, setUsername]
        = useState('')

    const [password, setPassword]
        = useState('')

    const [errorMessage, setErrorMessage]
        = useState('')

    const navigate = useNavigate()

    const handleRegister = async () => {

        try {

            setErrorMessage('')

            await api.post(
                '/Auth/register',
                {
                    username,
                    password
                }
            )

            toast.success(
                'Account created successfully. Please login.'
            )

            setTimeout(() => {

                navigate('/login')

            }, 1500)
        }
        catch (error) {

            console.error(error)

            setErrorMessage(

                error.response?.data ||

                'Registration failed'
            )
        }
    }

    return (

        <div className="login-page">

            <div className="login-card">

                <h1 className="login-title">

                    Create Account

                </h1>

                <p className="login-subtitle">

                    Register to access the
                    Fraud Detection Dashboard

                </p>

                <div className="input-group">

                    <label>Username</label>

                    <input
                        type="text"
                        value={username}
                        onChange={(e) =>
                            setUsername(
                                e.target.value
                            )
                        }
                    />

                </div>

                <div className="input-group">

                    <label>Password</label>

                    <input
                        type="password"
                        value={password}
                        onChange={(e) =>
                            setPassword(
                                e.target.value
                            )
                        }
                    />

                </div>

                {
                    errorMessage && (

                        <div className="error-message">

                            {errorMessage}

                        </div>
                    )
                }

                <button
                    className="login-button"
                    onClick={handleRegister}
                >

                    Register

                </button>

                <p
                    style={{
                        textAlign: 'center',
                        marginTop: '20px'
                    }}
                >

                    Already have an account?

                    <Link
                        to="/login"
                        style={{
                            marginLeft: '5px'
                        }}
                    >

                        Login

                    </Link>

                </p>

            </div>

        </div>
    )
}

export default RegisterPage