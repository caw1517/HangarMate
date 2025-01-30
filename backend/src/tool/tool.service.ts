import { Injectable } from '@nestjs/common';
import { Tool } from "./interfaces/tool.interface";

@Injectable()
export class ToolService {
    private readonly tools: Tool[] = [
        {
            toolName: "Torque",
            toolSerial: "1234567890",
        }
    ];

    getTools(): Tool[] {
        return this.tools;
    }
}
