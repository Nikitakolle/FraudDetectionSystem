import {

    LineChart,
    Line,
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    ResponsiveContainer

} from 'recharts'

function FraudTrendChart({ history }) {

    // ================= GROUP DATA BY DATE =================

    const groupedData = {}

    history.forEach((item) => {

        const date =
            new Date(item.createdAt)
                .toLocaleDateString()

        if (!groupedData[date]) {

            groupedData[date] = {

                date: date,

                fraudCount: 0
            }
        }

        if (item.isFraud) {

            groupedData[date].fraudCount += 1
        }
    })

    // ================= CONVERT OBJECT TO ARRAY =================

    const chartData =
        Object.values(groupedData)

    // ================= UI =================

    return (

        <div
            style={{
                width: '100%',
                height: 400,
                backgroundColor: 'white',
                borderRadius: '12px',
                padding: '20px',
                boxShadow:
                    '0 4px 10px rgba(0,0,0,0.08)'
            }}
        >

            <h2
                style={{
                    textAlign: 'center'
                }}
            >
                Fraud Trend Analysis
            </h2>

            <ResponsiveContainer>

                <LineChart data={chartData}>

                    <CartesianGrid strokeDasharray="3 3" />

                    <XAxis dataKey="date" />

                    <YAxis />

                    <Tooltip />

                    <Line
                        type="monotone"
                        dataKey="fraudCount"
                        stroke="#d32f2f"
                        strokeWidth={3}
                    />

                </LineChart>

            </ResponsiveContainer>

        </div>
    )
}

export default FraudTrendChart