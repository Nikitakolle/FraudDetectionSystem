import './PredictionCard.css'

function PredictionCard({
    isFraud,
    fraudProbability,
    recommendation,
    reason
}) {

    const percentage =
        (fraudProbability * 100).toFixed(2)

    return (

        <div
            className={
                isFraud
                    ? 'prediction-card fraud'
                    : 'prediction-card safe'
            }
        >

            <h2>
                {
                    isFraud
                        ? 'HIGH FRAUD RISK'
                        : 'SAFE TRANSACTION'
                }
            </h2>

            <p>

                Fraud Probability:

                <strong>
                    {' '}{percentage}%
                </strong>

            </p>

            <p>

                Risk Level:

                <strong>

                    {
                        percentage > 80
                            ? ' Critical'
                            : percentage > 50
                                ? ' High'
                                : percentage > 25
                                    ? ' Medium'
                                    : ' Low'
                    }

                </strong>

            </p>
            <p>

                Recommendation:

                <strong>
                    {' '}
                    {recommendation}
                </strong>

            </p>

            <p>

                Reason:

                <strong>
                    {' '}
                    {reason}
                </strong>

            </p>

        </div>
    )
}

export default PredictionCard