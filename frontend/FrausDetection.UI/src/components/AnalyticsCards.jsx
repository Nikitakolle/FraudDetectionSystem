import './AnalyticsCards.css'

function AnalyticsCards({ history }) {

    const totalTransactions =
        history.length

    const fraudTransactions =
        history.filter(
            (item) => item.isFraud
        ).length

    const safeTransactions =
        totalTransactions - fraudTransactions

    const fraudRate =
        totalTransactions > 0
            ? (
                (fraudTransactions / totalTransactions) * 100
            ).toFixed(2)
            : 0

    return (

        <div className="analytics-grid">

            
            <div className="analytics-card rate-card">

                <h3>
                    Total Transactions
                </h3>

                <p>
                    {totalTransactions}
                </p>

            </div>

            <div className="analytics-card fraud-card">

                <h3>
                    Fraud Transactions
                </h3>

                <p>
                    {fraudTransactions}
                </p>

            </div>

            <div className="analytics-card safe-card">

                <h3>
                    Safe Transactions
                </h3>

                <p>
                    {safeTransactions}
                </p>

            </div>

            <div className="analytics-card">

                <h3>
                    Fraud Rate
                </h3>

                <p>
                    {fraudRate}%
                </p>

            </div>

        </div>
    )
}

export default AnalyticsCards