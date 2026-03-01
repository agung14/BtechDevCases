import axios from './axiosInstance'

const API_URL = 'https://localhost:7055/api/auth' // sesuaikan dengan API kamu

export const login = async (email: string, password: string) => {
  const response = await axios.post(`${API_URL}/login`, {
    email,
    password,
  })

  const token = response.data.token
  localStorage.setItem('token', token)
  startAutoLogout()

  return token
}

export const register = async (email: string, password: string) => {
  return axios.post(`${API_URL}/register`, {
    email,
    password,
  })
}

// ✅ Tambahkan ini
export const getToken = (): string | null => {
  return localStorage.getItem('token')
}

// ✅ Tambahkan ini
export const parseJwt = (token: string) => {
  const base64Url = token.split('.')[1]!
  const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
  const jsonPayload = decodeURIComponent(
    atob(base64)
      .split('')
      .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
      .join(''),
  )

  return JSON.parse(jsonPayload)
}

export const isTokenExpired = (token: string): boolean => {
  try {
    const decoded = parseJwt(token)
    const currentTime = Date.now() / 1000

    return decoded.exp < currentTime
  } catch {
    return true
  }
}

export const startAutoLogout = () => {
  const token = localStorage.getItem('token')
  if (!token) return

  const decoded = parseJwt(token)
  const currentTime = Date.now() / 1000
  const timeLeft = decoded.exp - currentTime

  if (timeLeft <= 0) {
    localStorage.removeItem('token')
    window.location.href = '/login'
    return
  }

  setTimeout(() => {
    localStorage.removeItem('token')
    window.location.href = '/login'
  }, timeLeft * 1000)
}

export const Logout = () => {
  localStorage.removeItem('token')
  return
}
