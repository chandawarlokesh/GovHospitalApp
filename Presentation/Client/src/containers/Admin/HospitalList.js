import React, { useState, useEffect } from "react";
import map from "lodash/map";
import head from "lodash/head";
import omit from "lodash/omit";
import startCase from "lodash/startCase";
import { useFetch } from "utils/hooks";

const HospitalList = () => {
    const [hospitals, setHospitals] = useState();
    const { loading, doFetch } = useFetch({
        url: `/api/hospitals`,
        onComplete: setHospitals,
    });

    useEffect(() => {
        if (hospitals) {
            return;
        }
        doFetch();
    }, [hospitals, doFetch]);

    if (loading || !hospitals) {
        return null;
    }
    const formattedHospitals = map(hospitals, (hospital) => ({
        ...omit(hospital, ["address", "id"]),
        ...hospital.address,
    }));

    return (
        <table className="w-full text-left">
            <thead>
                <tr className="bg-gray-100">
                    {map(head(formattedHospitals), (value, key) => (
                        <th className="border">{startCase(key)}</th>
                    ))}
                </tr>
            </thead>
            <tbody>
                {map(formattedHospitals, (hospital) => (
                    <tr>
                        {map(hospital, (value) => (
                            <td className="border">{value}</td>
                        ))}
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

export default HospitalList;
