import axios from 'axios'

const api = axios.create({

    baseURL: 'https://localhost:7206/api'
})

// ================= ADD TOKEN AUTOMATICALLY =================

api.interceptors.request.use(

    (config) => {

        const token =
            localStorage.getItem('token')
        console.log(token)

        if (token) {

            config.headers.Authorization =
                `Bearer ${token}`
        }

        return config
    }
)

export default api