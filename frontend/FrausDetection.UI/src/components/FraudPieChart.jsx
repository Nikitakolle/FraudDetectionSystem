import {

    PieChart,
    Pie,
    Cell,
    Tooltip,
    Legend,
    ResponsiveContainer

} from 'recharts'

function FraudPieChart({ history }) {

    // ================= CALCULATIONS =================

    const fraudCount =
        history.filter(
            (item) => item.isFraud
        ).length

    const safeCount =
        history.length - fraudCount

    // ================= CHART DATA =================

    const data = [

        {
            name: 'Fraud',
            value: fraudCount
        },

        {
            name: 'Safe',
            value: safeCount
        }
    ]

    // ================= COLORS =================

    const COLORS = [

        '#d32f2f',
        '#2e7d32'
    ]

    // ================= UI =================

    return (

        <div
            style={{
                maxWidth: '100%',
                height: 400,
                backgroundColor: 'white',
                borderRadius: '12px',
                padding: '20px',
                boxShadow:
                    '0 4px 10px rgba(0,0,0,0.08)',
                marginBottom: '30px'
            }}
        >

            <h2
                style={{
                    textAlign: 'center'
                }}
            >
                Fraud Distribution
            </h2>

            <ResponsiveContainer>

                <PieChart>

                    <Pie
                        data={data}
                        cx="50%"
                        cy="50%"
                        outerRadius={100}
                        dataKey="value"
                        label
                    >

                        {
                            data.map(
                                (entry, index) => (

                                    <Cell
                                        key={index}
                                        fill={
                                            COLORS[index]
                                        }
                                    />
                                )
                            )
                        }

                    </Pie>

                    <Tooltip />

                    <Legend
                        verticalAlign="bottom"
                        height={60}
                    />

                </PieChart>

            </ResponsiveContainer>

        </div>
    )
}

export default FraudPieChart