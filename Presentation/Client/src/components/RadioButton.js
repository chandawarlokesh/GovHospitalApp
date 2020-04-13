import React, { forwardRef } from "react";
import styled from "styled-components";

const StyledRadioButton = styled.div`
    display: flex;
    &:hover input ~ span {
        background-color: #4a5568;
    }
    input:checked ~ span {
        background-color: #a0aec0;
    }
`;

const CheckMark = styled.span`
    top: 0.4rem;
    &:after {
        content: "";
        position: absolute;
        display: none;
        top: 0.25rem;
        left: 0.25rem;
        width: 0.5rem;
        height: 0.5rem;
        border-radius: 50%;
        background: white;
    }
`;

const Input = styled.input`
    &:checked ~ span:after {
        display: block;
    }
`;

const RadioButton = forwardRef(({ className, label, ...props }, ref) => {
    return (
        <StyledRadioButton className={className}>
            <label className="select-none relative pl-5 mb-3 cursor-pointer text-lg">
                {label}
                <Input
                    {...props}
                    ref={ref}
                    type="radio"
                    className="absolute opacity-0 cursor-pointer"
                />
                <CheckMark className="absolute left-0 h-4 w-4 rounded-full bg-gray-300" />
            </label>
        </StyledRadioButton>
    );
});

export default RadioButton;
