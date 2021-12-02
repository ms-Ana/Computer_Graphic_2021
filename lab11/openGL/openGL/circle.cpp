#include <gl/glew.h>
#include <SFML/OpenGL.hpp>
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>

#include <iostream>
#include <vector>


GLuint Program;

GLint Attrib_vertex;

GLint Attrib_color;

GLint Unif_color;

GLuint VBO_position;

GLuint VBO_color;

struct Vertex
{
	GLfloat x;
	GLfloat y;
};

struct Color
{
	GLfloat R;
	GLfloat G;
	GLfloat B;
	GLfloat A;
};

GLint Unif_scale;
float scale[2] = { 1.0f, 1.0f };

const char* VertexShaderSource = R"(
    #version 330 core
    in vec2 coord;
    in vec4 color;
	uniform vec2 scale;
    out vec4 vert_color;
    void main() {
        vert_color = color;
		vec3 pos = vec3(coord, 0.0);
		pos = pos * mat3(
						scale[0], 0, 0,
						0, scale[1], 0,
						0, 0, 1
						);		
        gl_Position = vec4(pos, 1.0);
    }
)";

const char* FragShaderSource = R"(
    #version 330 core
    in vec4 vert_color;
    out vec4 color;
    void main() {
        color = vert_color;
    }
)";


void Init();
void Draw();
void Release();


int main() {
	sf::Window window(sf::VideoMode(600, 600), "My OpenGL window", sf::Style::Default, sf::ContextSettings(24));
	window.setVerticalSyncEnabled(true);

	window.setActive(true);

	glewInit();
	glEnable(GL_DEPTH_TEST);

	Init();

	while (window.isOpen()) {
		sf::Event event;
		while (window.pollEvent(event)) {
			if (event.type == sf::Event::Closed) {
				window.close();
			}
			else if (event.type == sf::Event::Resized) {
				glViewport(0, 0, event.size.width, event.size.height);
			}

			else if (event.type == sf::Event::KeyPressed) {
				switch (event.key.code) {
				case (sf::Keyboard::Left): scale[0] -= 0.1; break;
				case (sf::Keyboard::Right): scale[0] += 0.1; break;
				case (sf::Keyboard::Down): scale[1] -= 0.1; break;
				case (sf::Keyboard::Up): scale[1] += 0.1; break;
				}
			}
		}

		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		Draw();

		window.display();
	}

	Release();
	return 0;
}


void checkOpenGLerror() {
	GLenum errCode;
	if ((errCode = glGetError()) != GL_NO_ERROR)
		std::cout << "OpenGl error!: " << errCode << std::endl;
}

void ShaderLog(unsigned int shader)
{
	int infologLen = 0;
	int charsWritten = 0;
	char* infoLog;
	glGetShaderiv(shader, GL_INFO_LOG_LENGTH, &infologLen);
	if (infologLen > 1)
	{
		infoLog = new char[infologLen];
		if (infoLog == NULL)
		{
			std::cout << "ERROR: Could not allocate InfoLog buffer" << std::endl;
			exit(1);
		}
		glGetShaderInfoLog(shader, infologLen, &charsWritten, infoLog);
		std::cout << "InfoLog: " << infoLog << "\n\n\n";
		delete[] infoLog;
	}
}

Color HSV2RGB(float H, float S, float V) {
	float s = S / 100;
	float v = V / 100;
	float C = s * v;
	float X = C * (1 - abs(fmod(H / 60.0, 2) - 1));
	float m = v - C;
	float r, g, b;
	if (0 <= H && H < 60) 
		r = C, g = X, b = 0;
	else if (60 <= H && H < 120)
		r = X, g = C, b = 0;
	else if (120 <= H && H < 180) 
		r = 0, g = C, b = X;
	else if (180 <= H && H < 240) 
		r = 0, g = X, b = C;
	else if (240 <= H && H < 300) 
		r = X, g = 0, b = C;
	else 
		r = C, g = 0, b = X;
	
	return { r+m, g+m, b+m, 1.0 };
}

void InitVBO()
{
	glGenBuffers(1, &VBO_position);
	glGenBuffers(1, &VBO_color);


	const int tr = 360;

	Vertex triangle[tr + 2];
	triangle[0] = { 0.0, 0.0 };
	for (int i = 1; i <= tr + 1; ++i)
	{
		float t = 2.0f * 3.1415f * i / 360;
		triangle[i]= { 0.5f * std::cosf(t), 0.5f * std::sinf(t) };
	}

	std::vector<Color> colors_vec;
	Color colors[tr + 2];
	colors[0] = { 1.0, 1.0, 1.0, 1.0 };
	
	for (int i = 1; i <= tr + 1; ++i)
	{
		colors[i] = HSV2RGB(i, 100, 100);
	
	}
	
	glBindBuffer(GL_ARRAY_BUFFER, VBO_position);
	glBufferData(GL_ARRAY_BUFFER, sizeof(triangle), triangle, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
	glBufferData(GL_ARRAY_BUFFER, sizeof(colors), colors, GL_STATIC_DRAW);
	checkOpenGLerror();
}


void InitShader() {
	GLuint vShader = glCreateShader(GL_VERTEX_SHADER);

	glShaderSource(vShader, 1, &VertexShaderSource, NULL);
	glCompileShader(vShader);
	std::cout << "vertex shader \n";
	ShaderLog(vShader);

	GLuint fShader = glCreateShader(GL_FRAGMENT_SHADER);

	glShaderSource(fShader, 1, &FragShaderSource, NULL);

	glCompileShader(fShader);
	std::cout << "fragment shader \n";
	ShaderLog(fShader);

	Program = glCreateProgram();
	glAttachShader(Program, vShader);
	glAttachShader(Program, fShader);

	glLinkProgram(Program);

	int link_ok;
	glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
	if (!link_ok)
	{
		std::cout << "error attach shaders \n";
		return;
	}

	Attrib_vertex = glGetAttribLocation(Program, "coord");
	if (Attrib_vertex == -1)
	{
		std::cout << "could not bind attrib coord" << std::endl;
		return;
	}

	Attrib_color = glGetAttribLocation(Program, "color");
	if (Attrib_color == -1)
	{
		std::cout << "could not bind attrib color" << std::endl;
		return;
	}

	const char* unif_name = "scale";
	Unif_color = glGetUniformLocation(Program, unif_name);
	if (Unif_color == -1)
	{
		std::cout << "could not bind uniform " << unif_name << std::endl;
		return;
	}

	checkOpenGLerror();
}

void Init() {
	InitShader();
	InitVBO();
}


void Draw() {
	glUseProgram(Program);

	glUniform2fv(Unif_scale, 1, scale);

	glEnableVertexAttribArray(Attrib_vertex);
	glEnableVertexAttribArray(Attrib_color);

	glBindBuffer(GL_ARRAY_BUFFER, VBO_position);
	glVertexAttribPointer(Attrib_vertex, 2, GL_FLOAT, GL_FALSE, 0, 0);

	glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
	glVertexAttribPointer(Attrib_color, 4, GL_FLOAT, GL_FALSE, 0, 0);

	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glDrawArrays(GL_TRIANGLE_FAN, 0, 362);

	glDisableVertexAttribArray(Attrib_vertex);
	glDisableVertexAttribArray(Attrib_color);

	glUseProgram(0);
	checkOpenGLerror();
}


void ReleaseShader() {
	glUseProgram(0);
	glDeleteProgram(Program);
}

void ReleaseVBO()
{
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glDeleteBuffers(1, &VBO_position);
	glDeleteBuffers(1, &VBO_color);
}

void Release() {
	ReleaseShader();
	ReleaseVBO();
}