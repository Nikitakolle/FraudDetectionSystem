import { useState } from 'react'
import api from '../axiosConfig'
import PredictionCard from '../components/PredictionCard'
import Navbar from '../components/Navbar'
import { toast } from 'react-toastify'
import LoadingSpinner from '../components/LoadingSpinner'
import '../App.css'

function HomePage() {

    // ================= STATES =================

    const [transactionAmount, setTransactionAmount] = useState('')

    const [merchantRiskScore, setMerchantRiskScore] = useState('')

    const [ipRiskScore, setIpRiskScore] = useState('')

    const [isInternational, setIsInternational] = useState(false)

    const [deviceType, setDeviceType] = useState('Mobile')

    const [paymentChannel, setPaymentChannel] = useState('Online')

    const [creditScoreBand, setCreditScoreBand] = useState('Good')

    const [kycLevel, setKycLevel] = useState('Full')

    const [postAuthRiskScore, setPostAuthRiskScore] = useState('')

    const [predictionResult, setPredictionResult] = useState(null)

    const [errorMessage, setErrorMessage] = useState('')

    const [isLoading, setIsLoading] = useState(false)
    const [uploadMessage, setUploadMessage]
        = useState('')
    // ================= PREDICTION =================

    const handlePrediction = async () => {

        setErrorMessage('')

        // ================= VALIDATION =================

        if (!transactionAmount) {

            setErrorMessage(
                'Transaction amount is required'
            )

            return
        }

        if (Number(transactionAmount) <= 0) {

            setErrorMessage(
                'Transaction amount must be greater than 0'
            )

            return
        }

        if (!merchantRiskScore) {

            setErrorMessage(
                'Merchant risk score is required'
            )

            return
        }

        if (
            Number(merchantRiskScore) < 0 ||
            Number(merchantRiskScore) > 1
        ) {

            setErrorMessage(
                'Merchant risk score must be between 0 and 1'
            )

            return
        }

        if (!ipRiskScore) {

            setErrorMessage(
                'IP risk score is required'
            )

            return
        }

        if (
            Number(ipRiskScore) < 0 ||
            Number(ipRiskScore) > 1
        ) {

            setErrorMessage(
                'IP risk score must be between 0 and 1'
            )

            return
        }

        if (!postAuthRiskScore) {

            setErrorMessage(
                'Post auth risk score is required'
            )

            return
        }

        if (
            Number(postAuthRiskScore) < 0 ||
            Number(postAuthRiskScore) > 1
        ) {

            setErrorMessage(
                'Post auth risk score must be between 0 and 1'
            )

            return
        }

        setIsLoading(true)

        // ================= API CALL =================

        try {

            const requestData = {

                transactionAmount: Number(transactionAmount),

                merchantRiskScore: Number(merchantRiskScore),

                ipRiskScore: Number(ipRiskScore),

                isInternational: isInternational,

                creditScoreBand: creditScoreBand,

                kycLevel: kycLevel,

                paymentChannel: paymentChannel,

                deviceType: deviceType,

                postAuthRiskScore: Number(postAuthRiskScore)
            }

            const response = await api.post(
                
                '/FraudDetection/predict',
                requestData
            )

            setPredictionResult(response.data)
            toast.success(
                'Fraud prediction completed'
            )

            setIsLoading(false)
        }
        catch (error) {

            setIsLoading(false)

            console.error(error)

            if (error.response?.data?.errors) {

                const validationErrors =
                    error.response.data.errors

                const firstErrorKey =
                    Object.keys(validationErrors)[0]

                const firstErrorMessage =
                    validationErrors[firstErrorKey][0]

                setErrorMessage(firstErrorMessage)
            }
            else {

                toast.error(
                    'Fraud prediction failed'
                )
            }
        }
    }

    const handleFileUpload = async (event) => {

        try {

            const file =
                event.target.files[0]

            if (!file) {

                return
            }

            // ================= CREATE FORMDATA =================

            const formData = new FormData()

            formData.append('file', file)

            // ================= SEND TO API =================

            const response = await api.post(

                '/FraudDetection/upload-csv',

                formData,

                {
                    headers: {
                        'Content-Type':
                            'multipart/form-data'
                    }
                }
            )

            // ================= SUCCESS =================

            toast.success(
                response.data.message
            )

            fetchHistory()
        }
        catch (error) {

            console.error(error)

            toast.error(
                'CSV upload failed'
            )
        }
    }

    
    // ================= UI =================

    return (
          
        <>
          <Navbar />

          <div className="app-container">
           

            <div className="form-card homepage-card">

                <div className="top-bar">

                    <h1 className="form-title">
                        Fraud Detection System
                    </h1>

                    

                </div>

                <p className="form-subtitle">
                        Real-Time Fraud Analysis using C#, ASP.NET Core Web API, React, SQL Server, and ML.NET
                </p>

                {/* ================= TRANSACTION AMOUNT ================= */}

                <div className="input-group">

                    <label>Transaction Amount</label>

                    <input
                        type="number"
                        placeholder="Enter transaction amount"
                        value={transactionAmount}
                        onChange={(event) =>
                            setTransactionAmount(event.target.value)
                        }
                    />

                </div>

                {/* ================= MERCHANT RISK ================= */}

                <div className="input-group">

                    <label>Merchant Risk Score</label>

                    <input
                        type="number"
                        step="0.01"
                        placeholder="Enter merchant risk score"
                        value={merchantRiskScore}
                        onChange={(event) =>
                            setMerchantRiskScore(event.target.value)
                        }
                    />

                </div>

                {/* ================= IP RISK ================= */}

                <div className="input-group">

                    <label>IP Risk Score</label>

                    <input
                        type="number"
                        step="0.01"
                        placeholder="Enter IP risk score"
                        value={ipRiskScore}
                        onChange={(event) =>
                            setIpRiskScore(event.target.value)
                        }
                    />

                </div>

                {/* ================= DEVICE TYPE ================= */}

                <div className="input-group">

                    <label>Device Type</label>

                    <select
                        value={deviceType}
                        onChange={(event) =>
                            setDeviceType(event.target.value)
                        }
                    >

                        <option value="Mobile">
                            Mobile
                        </option>

                        <option value="Desktop">
                            Desktop
                        </option>

                        <option value="Tablet">
                            Tablet
                        </option>

                    </select>

                </div>

                {/* ================= PAYMENT CHANNEL ================= */}

                <div className="input-group">

                    <label>Payment Channel</label>

                    <select
                        value={paymentChannel}
                        onChange={(event) =>
                            setPaymentChannel(event.target.value)
                        }
                    >

                        <option value="Online">
                            Online
                        </option>

                        <option value="ATM">
                            ATM
                        </option>

                        <option value="POS">
                            POS
                        </option>

                    </select>

                </div>

                {/* ================= CREDIT SCORE ================= */}

                <div className="input-group">

                    <label>Credit Score Band</label>

                    <select
                        value={creditScoreBand}
                        onChange={(event) =>
                            setCreditScoreBand(event.target.value)
                        }
                    >

                        <option value="Poor">
                            Poor
                        </option>

                        <option value="Average">
                            Average
                        </option>

                        <option value="Good">
                            Good
                        </option>

                        <option value="Excellent">
                            Excellent
                        </option>

                    </select>

                </div>

                {/* ================= KYC LEVEL ================= */}

                <div className="input-group">

                    <label>KYC Level</label>

                    <select
                        value={kycLevel}
                        onChange={(event) =>
                            setKycLevel(event.target.value)
                        }
                    >

                        <option value="None">
                            None
                        </option>

                        <option value="Partial">
                            Partial
                        </option>

                        <option value="Full">
                            Full
                        </option>

                    </select>

                </div>

                {/* ================= POST AUTH RISK ================= */}

                <div className="input-group">

                    <label>Post Auth Risk Score</label>

                    <input
                        type="number"
                        step="0.01"
                        placeholder="Enter post auth risk score"
                        value={postAuthRiskScore}
                        onChange={(event) =>
                            setPostAuthRiskScore(event.target.value)
                        }
                    />

                </div>

                {/* ================= INTERNATIONAL ================= */}

                <div className="input-group">

                    <label>

                        <input
                            type="checkbox"
                            checked={isInternational}
                            onChange={(event) =>
                                setIsInternational(
                                    event.target.checked
                                )
                            }
                        />

                        International Transaction

                    </label>

                </div>

                {/* ================= ERROR MESSAGE ================= */}

                {
                    errorMessage && (

                        <div className="error-message">

                            {errorMessage}

                        </div>
                    )
                }

                {/* ================= BUTTON ================= */}
                <div className="upload-container">

                    <label className="upload-label">

                        Upload CSV File

                    </label>

                    <input
                        type="file"
                        accept=".csv"
                        onChange={handleFileUpload}
                    />

                </div>

                {
                    uploadMessage && (

                        <div className="success-message">

                            {uploadMessage}

                        </div>
                    )
                }
                <button
                    className="predict-button"
                    onClick={handlePrediction}
                    disabled={isLoading}
                >

                        {
                            isLoading ? (

                                <div
                                    style={{
                                        display: 'flex',
                                        justifyContent: 'center',
                                        alignItems: 'center',
                                        gap: '10px'
                                    }}
                                >

                                    <LoadingSpinner />

                                    Predicting...

                                </div>

                            ) : (

                                'Predict Fraud'
                            )
                        }

                </button>

                {/* ================= PREDICTION RESULT ================= */}

                {
                    predictionResult && (

                        <PredictionCard
                                isFraud={predictionResult.isFraud}
                                fraudProbability={
                                    predictionResult.fraudProbability
                                }
                                recommendation={predictionResult.recommendation}
                                reason={predictionResult.reason}
                              
                        />
                    )
                }

            </div>

          </div>

        </>
    )
}

export default HomePage