import React, { useState } from "react";
import Logo from "components/Logo";
import Hospital from "./Hospital";
import Patient from "./Patient";
import { useData } from "containers/DataProvider";

const Home = () => {
    const { id: patientId } = useData();
    const [patientData, setPatientData] = useState();
    const [hospitalData, setHospitalData] = useState();

    return (
        <div className="container m-auto">
            <div className="w-full p-4">
                <Logo className="p-4" />
                <div className="flex flex-col md:flex-row">
                    <div className="w-1/2">
                        <Patient
                            id={patientId}
                            data={patientData}
                            updateData={setPatientData}
                        />
                    </div>
                    <div className="w-1/2">
                        <Hospital
                            id={patientData && patientData.hospitalId}
                            data={hospitalData}
                            updateData={setHospitalData}
                        />
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Home;
