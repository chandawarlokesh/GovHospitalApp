import React, { useState } from "react";
import cx from "classnames";
import PatientList from "./PatientList";
import HospitalList from "./HospitalList";

const TabOption = ({ index, selected, onSelect, title }) => (
    <button
        className={cx("p-4 mr-2", {
            "bg-gray-400": index === selected,
        })}
        onClick={() => onSelect(index)}
    >
        {title}
    </button>
);

const TabBody = ({ index, selected, children }) =>
    index === selected && <div className="p-4">{children}</div>;

const Admin = () => {
    const [tab, selectTab] = useState(1);
    return (
        <>
            <div className="overflow-hidden bg-gray-300">
                <TabOption
                    index={1}
                    selected={tab}
                    onSelect={selectTab}
                    title="Patients"
                />
                <TabOption
                    index={2}
                    selected={tab}
                    onSelect={selectTab}
                    title="Hospital"
                />
            </div>
            <TabBody index={1} selected={tab}>
                <h3 className="text-2xl pb-4 text-center font-bold">
                    {"Patients"}
                </h3>
                <PatientList />
            </TabBody>
            <TabBody index={2} selected={tab}>
                <h3 className="text-2xl pb-4 text-center font-bold">
                    {"Hospitals"}
                </h3>
                <HospitalList />
            </TabBody>
        </>
    );
};

export default Admin;
