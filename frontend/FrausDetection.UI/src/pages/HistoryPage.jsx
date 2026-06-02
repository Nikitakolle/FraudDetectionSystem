import FraudPieChart from '../components/FraudPieChart'
import FraudTrendChart from '../components/FraudTrendChart'
import AnalyticsCards from '../components/AnalyticsCards'
import Navbar from '../components/Navbar'
import { toast }
    from 'react-toastify'
import api from '../axiosConfig'
import { useEffect, useState } from 'react'
import TransactionHistoryTable from '../components/TransactionHistoryTable'
import '../App.css'

function HistoryPage() {

    const [history, setHistory] = useState([])

    const [errorMessage, setErrorMessage] = useState('')
    const [searchAmount, setSearchAmount]
        = useState('')

    const [filterType, setFilterType]
        = useState('All')
    const [exportType, setExportType]
        = useState('All')
    // ================= FETCH HISTORY =================

    const fetchHistory = async () => {

        try {

            const response = await api.get(
                '/FraudDetection/history'
            )

            setHistory(response.data)
        }
        catch (error) {

            console.error(error)

            toast.error(
                'Failed to load transaction history'
            )
        }
    }

    const exportToCSV = () => {

        // ================= CSV HEADERS =================

        const headers = [

            'Amount',
            'Fraud Probability',
            'Status',
            'Date'
        ]

        // ================= CSV ROWS =================

        const rows = exportData.map((item) => [

            item.transactionAmount,

            (
                item.fraudProbability * 100
            ).toFixed(2) + '%',

            item.isFraud
                ? 'Fraud'
                : 'Safe',

            new Date(
                item.createdAt
            ).toLocaleString()
        ])

        // ================= COMBINE DATA =================

        const csvContent = [

            headers,

            ...rows

        ]
            .map((row) => row.join(','))

            .join('\n')

        // ================= CREATE FILE =================

        const blob = new Blob(

            [csvContent],

            {
                type: 'text/csv'
            }
        )

        // ================= CREATE DOWNLOAD LINK =================

        const url =
            window.URL.createObjectURL(blob)

        const link =
            document.createElement('a')

        link.href = url

        link.download =
            'transaction-history.csv'

        // ================= DOWNLOAD FILE =================

        link.click()
        toast.success(
            'CSV exported successfully'
        )

        // ================= CLEANUP =================

        window.URL.revokeObjectURL(url)
    }
    // ================= LOAD HISTORY =================

    useEffect(() => {

        fetchHistory()

    }, [])
    
    const filteredHistory = history.filter(
        (item) => {

            // ================= SEARCH =================

            const matchesSearch =

                item.transactionAmount
                    .toString()
                    .includes(searchAmount)

            // ================= FILTER =================

            if (filterType === 'Fraud') {

                return matchesSearch &&
                    item.isFraud
            }

            if (filterType === 'Safe') {

                return matchesSearch &&
                    !item.isFraud
            }

            return matchesSearch
        }
    )

    const exportData = history.filter(
        (item) => {

            if (exportType === 'Fraud') {

                return item.isFraud
            }

            if (exportType === 'Safe') {

                return !item.isFraud
            }

            return true
        }
    )
    // ================= UI =================

    return (

        <>
            <Navbar />

          <div className="app-container">

            <div className="form-card">

                <div className="top-bar">

                    <h1 className="form-title">
                        Transaction History
                    </h1>

                    

                </div>
                <div className="filter-container">
                        <div className="filter-left">
                            <span className="toolbar-label">

                                Filter

                            </span>
                    <input
                        type="text"
                        placeholder="Search by amount"
                        value={searchAmount}
                        onChange={(event) =>
                            setSearchAmount(event.target.value)
                        }
                        className="search-input"
                    />

                    <select
                        value={filterType}
                        onChange={(event) =>
                            setFilterType(event.target.value)
                        }
                        className="filter-select"
                    >

                        <option value="All">
                            All Transactions
                        </option>

                        <option value="Fraud">
                            Fraud Only
                        </option>

                        <option value="Safe">
                            Safe Only
                        </option>

                        </select>
                    </div>  
                        <div className="filter-right">
                            <span className="toolbar-label">

                                Export

                            </span>
                            <select
                                value={exportType}
                                onChange={(event) =>
                                    setExportType(event.target.value)
                                }
                                className="export-select"
                            >
                                
                                <option value="All">
                                    All Transactions
                                </option>

                                <option value="Fraud">
                                    Fraud Only
                                </option>

                                <option value="Safe">
                                    Safe Only
                                </option>

                            </select>

                            <button
                                className="export-button"
                                onClick={exportToCSV}
                            >

                                Export CSV

                            </button>

                    </div> 

                        

                </div>
               

                {
                    errorMessage && (

                        <div className="error-message">

                            {errorMessage}

                        </div>
                    )
                }
                <div className="dashboard-layout">

                    <div className="table-section">

                        <TransactionHistoryTable
                            history={filteredHistory}
                        />

                    </div>

                    <div className="analytics-section">
                        <FraudPieChart
                            history={history}
                        />
                        <AnalyticsCards
                            history={history}
                        />
                        <FraudTrendChart
                            history={history}
                        />
                        

                    </div>

                </div>

            </div>

          </div>

        </>
    )
}

export default HistoryPage