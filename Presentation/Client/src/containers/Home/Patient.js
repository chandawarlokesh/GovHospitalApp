import React, { useCallback, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import axios from "axios";
import pickBy from "lodash/pickBy";
import { useFetch } from "utils/hooks";

const Patient = ({ patient, doFetch }) => {
    const { register, handleSubmit } = useForm({
        defaultValues: patient,
    });

    const [options, setOptions] = useState();

    const { loading, doFetch: fetchOptions } = useFetch({
        url: `/api/hospitals`,
        onComplete: setOptions,
    });

    useEffect(() => {
        if (options) {
            return;
        }
        fetchOptions();
    }, [options, fetchOptions]);

    const onSubmit = useCallback(
        (requestData) => {
            axios
                .post(
                    `/api/patients/${patient.id}`,
                    pickBy(requestData, (x) => x !== "null")
                )
                .then(() => {
                    doFetch();
                })
                .catch(() => {
                    window.alert("something went wrong! try again.");
                });
        },
        [patient, doFetch]
    );

    if (loading || !options) {
        return null;
    }

    return (
        <div className="w-full max-w-md shadow-lg m-auto">
            <h1 className="text-lg p-4 text-center font-bold bg-gray-200 uppercase">
                {"Patient Details"}
            </h1>
            <div className="flex flex-col p-6">
                <form onSubmit={handleSubmit(onSubmit)}>
                    <input
                        type="text"
                        name="id"
                        ref={register({
                            required: true,
                            maxLength: 255,
                        })}
                        className="h-10 w-full mb-4 p-2 border border-gray-300 hidden"
                    />
                    <input
                        type="text"
                        placeholder="Name"
                        name="name"
                        ref={register({
                            required: true,
                            maxLength: 255,
                        })}
                        className="h-10 w-full mb-4 p-2 border border-gray-300"
                    />
                    <input
                        type="tel"
                        placeholder="Mobile Number"
                        name="mobileNumber"
                        disabled
                        ref={register({
                            required: true,
                            minLength: 6,
                            maxLength: 12,
                        })}
                        className="h-10 w-full mb-4 p-2 border border-gray-300"
                    />
                    <input
                        type="datetime-local"
                        placeholder="Date Of Birth"
                        name="dateOfBirth"
                        ref={register({ required: true })}
                        className="h-10 w-full mb-4 p-2 border border-gray-300"
                    />
                    <select
                        name="gender"
                        ref={register()}
                        className="h-10 w-full mb-4 p-2 border border-gray-300 "
                        placeholder="Select a Gender..."
                    >
                        <option value={0}>{"Male"}</option>
                        <option value={1}>{"Female"}</option>
                        <option value={2}>{"Other"}</option>
                    </select>
                    <input
                        type="text"
                        placeholder="Street"
                        name="address.street"
                        ref={register({
                            required: true,
                            maxLength: 255,
                        })}
                        className="h-10 w-full mb-4 p-2 border border-gray-300"
                    />
                    <div className="flex flex-row">
                        <input
                            type="text"
                            placeholder="City"
                            name="address.city"
                            ref={register({
                                required: true,
                                maxLength: 80,
                            })}
                            className="h-10 w-full mb-4 p-2 border border-gray-300 mr-4"
                        />
                        <input
                            type="text"
                            placeholder="State"
                            name="address.state"
                            ref={register({
                                required: true,
                                maxLength: 50,
                            })}
                            className="h-10 w-full mb-4 p-2 border border-gray-300 mr-4"
                        />
                        <input
                            type="text"
                            placeholder="ZipCode"
                            name="address.zipCode"
                            ref={register({
                                required: true,
                                maxLength: 6,
                            })}
                            className="h-10 w-full mb-4 p-2 border border-gray-300 "
                        />
                    </div>
                    {options ? (
                        <select
                            name="hospitalId"
                            ref={register({
                                required: false,
                            })}
                            className="h-10 w-full mb-4 p-2 border border-gray-300 "
                            placeholder="Select a hospital..."
                        >
                            <option value={"null"}>
                                {"Select default hospital"}
                            </option>
                            {options.map((opt) => (
                                <option value={opt.id}>{opt.name}</option>
                            ))}
                        </select>
                    ) : (
                        <input
                            type="text"
                            name="hospitalId"
                            ref={register()}
                            className="hidden"
                        />
                    )}
                    <div className="flex justify-end">
                        <button
                            type="submit"
                            className="text-white bg-gray-500 px-3 py-2"
                        >
                            {"Update"}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

const FetchPatient = ({ id, data, updateData }) => {
    const { loading, doFetch } = useFetch({
        url: `/api/patients/${id}`,
        onComplete: updateData,
    });

    useEffect(() => {
        if (!id || data) {
            return;
        }
        doFetch();
    }, [id, data, doFetch]);

    if (loading || !data) {
        return null;
    }

    console.log(data);
    return <Patient patient={data} doFetch={doFetch} />;
};

export default FetchPatient;
