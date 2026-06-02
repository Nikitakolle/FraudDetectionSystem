import './TransactionHistoryTable.css'

function TransactionHistoryTable({ history }) {
    if (history.length === 0) {

        return (

            <div className="empty-state">

                No transactions found

            </div>
        )
    }

    return (

        <div className="history-container">

            <h2>
                Transaction History
            </h2>

            <table className="history-table">

                <thead>

                    <tr>

                        <th>Amount</th>

                        <th>Fraud Probability</th>

                        <th>Status</th>

                        <th>Recommendation</th>

                        <th>Reason</th>

                        <th>Date</th>

                    </tr>

                </thead>
                
                <tbody>

                    {
                        history.map((item) => {
                            console.log(item)
                            return (

                                <tr key={item.id}>

                                    <td>
                                        ${item.transactionAmount}
                                    </td>

                                    <td>
                                        {
                                            (
                                                item.fraudProbability * 100
                                            ).toFixed(2)
                                        }%
                                    </td>

                                    <td>

                                        <span
                                            className={
                                                item.isFraud
                                                    ? 'status fraud-status'
                                                    : 'status safe-status'
                                            }
                                        >

                                            {
                                                item.isFraud
                                                    ? 'Fraud'
                                                    : 'Safe'
                                            }

                                        </span>

                                    </td>
                                    <td>

                                        {item.recommendation}

                                    </td>

                                    <td>

                                        {item.reason}

                                    </td>
                                    <td>

                                        {
                                            new Date(
                                                item.createdAt
                                            ).toLocaleString()
                                        }

                                    </td>

                                </tr>
                            )
                        })
                    }
                </tbody>

            </table>

        </div>
    )
}

export default TransactionHistoryTable