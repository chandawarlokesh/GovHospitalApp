import React, { useContext, useState, useCallback, useMemo } from "react";

const PatientContext = React.createContext({});

const usePatient = () => {
    const [patient, setPatient] = useState({
        id: localStorage.getItem("patientId"),
    });

    const updatePatient = useCallback(
        (data = {}) => {
            localStorage.setItem("patientId", data.id || null);
            setPatient(data);
        },
        [setPatient]
    );

    return useMemo(
        () => ({
            ...patient,
            updatePatient,
        }),
        [patient, updatePatient]
    );
};

export const useData = () => useContext(PatientContext);

export const DataProvider = ({ children }) => {
    const patient = usePatient();
    return (
        <PatientContext.Provider value={patient}>
            {children}
        </PatientContext.Provider>
    );
};
