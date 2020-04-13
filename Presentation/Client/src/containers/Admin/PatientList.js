import React, { useState, useEffect } from "react";
import map from "lodash/map";
import head from "lodash/head";
import omit from "lodash/omit";
import startCase from "lodash/startCase";
import { useFetch } from "utils/hooks";

const PatientList = () => {
    const [patients, setPatients] = useState();
    const { loading, doFetch } = useFetch({
        url: `/api/patients`,
        onComplete: setPatients,
    });

    useEffect(() => {
        if (patients) {
            return;
        }
        doFetch();
    }, [patients, doFetch]);

    if (loading || !patients) {
        return null;
    }
    const formattedPatients = map(patients, (patient) => ({
        ...omit(patient, ["address", "id", "hospitalId"]),
        ...patient.address,
    }));

    return (
        <table className="w-full text-left">
            <thead>
                <tr className="bg-gray-100">
                    {map(head(formattedPatients), (value, key) => (
                        <th className="border">{startCase(key)}</th>
                    ))}
                </tr>
            </thead>
            <tbody>
                {map(formattedPatients, (patient) => (
                    <tr>
                        {map(patient, (value) => (
                            <td className="border">{value}</td>
                        ))}
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

export default PatientList;
