const PATIENT_ID = 'patientId'

export const isAuthenticated = () => {
	return localStorage.getItem(PATIENT_ID)
}

export const setAuthentication = (id) => {
	return localStorage.setItem(PATIENT_ID, id)
}
