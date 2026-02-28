export interface JwtPayload {
  email: string
  userId: string
  exp: number
}

export const getToken = (): string | null => {
  return localStorage.getItem('token')
}

export const setToken = (token: string) => {
  localStorage.setItem('token', token)
}

export const logout = () => {
  localStorage.removeItem('token')
}

export const parseJwt = (token: string): JwtPayload | null => {
  try {
    const parts = token.split('.')

    if (parts.length !== 3) {
      return null
    }

    const base64Payload = parts[1]

    if (!base64Payload) {
      return null
    }

    const payload = JSON.parse(atob(base64Payload))

    return payload
  } catch (error) {
    return null
  }
}